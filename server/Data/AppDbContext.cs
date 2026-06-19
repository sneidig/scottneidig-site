using Microsoft.EntityFrameworkCore;
using Server.Models;

namespace Server.Data;

// EF Core database context — the bridge between C# objects and the SQLite database.
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Project> Projects => Set<Project>();
    public DbSet<ProjectImage> ProjectImages => Set<ProjectImage>();
    public DbSet<ProjectPoint> ProjectPoints => Set<ProjectPoint>();
    public DbSet<SkillGroup> SkillGroups => Set<SkillGroup>();
    public DbSet<Skill> Skills => Set<Skill>();
    public DbSet<ContactMessage> ContactMessages => Set<ContactMessage>();
}
