using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Server.Data;
using Server.Models;
using Server.Services;

namespace Server.Controllers;

// Receives contact-form submissions: saves to the DB and (if configured) emails.
// POST /api/contact
[ApiController]
[Route("api/contact")]
public class ContactController : ControllerBase
{
    private readonly AppDbContext _db;
    private readonly EmailSender _email;

    public ContactController(AppDbContext db, EmailSender email)
    {
        _db = db;
        _email = email;
    }

    [HttpPost]
    [EnableRateLimiting("contact")] // max 5 per 10 min per IP
    public async Task<IActionResult> Post(ContactRequest req)
    {
        // Honeypot: bots fill the hidden "Website" field. Silently accept + discard.
        if (!string.IsNullOrWhiteSpace(req.Website))
            return Ok(new { ok = true });

        // Basic validation.
        if (string.IsNullOrWhiteSpace(req.Name) ||
            string.IsNullOrWhiteSpace(req.Email) || !req.Email.Contains('@') ||
            string.IsNullOrWhiteSpace(req.Message))
            return BadRequest(new { error = "Please fill in your name, a valid email, and a message." });

        if (req.Message.Length > 5000)
            return BadRequest(new { error = "Message is too long." });

        var msg = new ContactMessage
        {
            Name = req.Name.Trim(),
            Email = req.Email.Trim(),
            Message = req.Message.Trim(),
        };
        _db.ContactMessages.Add(msg);
        await _db.SaveChangesAsync();

        try {
            await _email.SendContactAsync(msg.Name, msg.Email, msg.Message);
        } catch (Exception ex) { 
            /* log it; message is already saved, so don't fail the request */ }

        return Ok(new { ok = true });
    }
}

