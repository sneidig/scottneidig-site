namespace Server.Models;

// A submitted contact-form message. Stored in the DB; optionally emailed.
public class ContactMessage
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string Email { get; set; } = "";
    public string Message { get; set; } = "";
    public DateTime CreatedUtc { get; set; } = DateTime.UtcNow;
}
