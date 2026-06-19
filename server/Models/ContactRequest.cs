namespace Server.Models;

// The incoming contact-form body. Website is the honeypot (hidden in the UI).
public record ContactRequest(string Name, string Email, string Message, string? Website);
