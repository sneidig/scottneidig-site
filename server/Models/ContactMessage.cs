using System.ComponentModel.DataAnnotations;

namespace Server.Models;

// A submitted contact-form message. Stored in the DB; optionally emailed.
public class ContactMessage
{
    public int Id { get; set; }

    [MaxLength(100)]
    public string Name { get; set; } = "";

    [MaxLength(200)]
    public string Email { get; set; } = "";

    [MaxLength(5000)]
    public string Message { get; set; } = "";

    public DateTime CreatedUtc { get; set; } = DateTime.UtcNow;
}
