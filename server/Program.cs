using System.Threading.RateLimiting;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using Server.Data;
using Server.Models;
using Server.Services;

var builder = WebApplication.CreateBuilder(args);

// --- Services ---

// SQL Server via EF Core. Dev uses local SQL Server; prod uses the connection
// string from configuration (e.g. Azure SQL), so dev and prod share one engine.
var connection = builder.Configuration.GetConnectionString("Default");
if (string.IsNullOrWhiteSpace(connection))
    throw new InvalidOperationException("Missing connection string 'Default'.");
builder.Services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(connection));

builder.Services.AddControllers();
builder.Services.AddScoped<EmailSender>();

// Allow the React dev server (Vite) to call the API during development.
const string DevCors = "DevCors";
builder.Services.AddCors(o => o.AddPolicy(DevCors, p =>
    p.WithOrigins("http://localhost:5173").AllowAnyHeader().AllowAnyMethod()));

// Rate limit the contact endpoint: max 5 submissions per 10 minutes per IP.
builder.Services.AddRateLimiter(o => o.AddPolicy("contact", ctx =>
    RateLimitPartition.GetFixedWindowLimiter(
        ctx.Connection.RemoteIpAddress?.ToString() ?? "unknown",
        _ => new FixedWindowRateLimiterOptions { PermitLimit = 5, Window = TimeSpan.FromMinutes(10) })));

var app = builder.Build();

// --- Create + seed the database on startup ---
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate(); // applies any pending EF Core migrations
    SeedData.Initialize(db);
}

app.UseCors(DevCors);
app.UseRateLimiter();

// Serve the built React app (wwwroot) in production.
app.UseDefaultFiles();
app.UseStaticFiles();

// Map attribute-routed controllers (ContentController, ContactController).
app.MapControllers();

// SPA fallback: any non-API route serves index.html (React handles routing).
app.MapFallbackToFile("index.html");

app.Run();
