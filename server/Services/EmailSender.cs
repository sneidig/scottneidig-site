using System.Net;
using System.Net.Mail;

namespace Server.Services;

// Sends contact-form notifications by email via SMTP.
// If SMTP isn't configured in appsettings, it quietly no-ops (the message is still
// saved to the database, so nothing is lost).
public class EmailSender
{
    private readonly IConfiguration _config;
    private readonly ILogger<EmailSender> _logger;

    public EmailSender(IConfiguration config, ILogger<EmailSender> logger)
    {
        _config = config;
        _logger = logger;
    }

    public async Task<bool> SendContactAsync(string name, string fromEmail, string message)
    {
        var smtp = _config.GetSection("Smtp");
        var host = smtp["Host"];
        var user = smtp["User"];
        var pass = smtp["Password"];
        var to = smtp["To"];

        // Not configured yet — skip sending. Message is already in the DB.
        if (string.IsNullOrWhiteSpace(host) || string.IsNullOrWhiteSpace(user) ||
            string.IsNullOrWhiteSpace(pass) || string.IsNullOrWhiteSpace(to))
        {
            _logger.LogInformation("SMTP not configured; contact message saved to DB only.");
            return false;
        }

        var port = int.TryParse(smtp["Port"], out var p) ? p : 587;

        using var client = new SmtpClient(host, port)
        {
            Credentials = new NetworkCredential(user, pass),
            EnableSsl = true,
        };

        var mail = new MailMessage(user, to)
        {
            Subject = $"Portfolio contact from {name}",
            Body = $"From: {name} <{fromEmail}>\n\n{message}",
        };
        mail.ReplyToList.Add(new MailAddress(fromEmail, name));

        await client.SendMailAsync(mail);
        return true;
    }
}
