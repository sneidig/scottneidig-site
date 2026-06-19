using Server.Models;

namespace Server.Data;

// Populates the database with the portfolio content the first time it runs.
public static class SeedData
{
    public static void Initialize(AppDbContext db)
    {
        if (db.Projects.Any()) return; // already seeded

        var jambalam = new Project
        {
            Title = "Jambalam — an AI-powered platform built on a custom nopCommerce plugin architecture, designed and operated solo",
            Lead = "Jambalam is a live community platform where jam-session musicians log what they " +
                   "played, where, and in what key — turning scattered musical knowledge into searchable " +
                   "data, maps, and trend visualizations. I architected it as a custom nopCommerce plugin — " +
                   "extending the platform through its plugin model rather than forking it — and took it from " +
                   "idea to production on my own: backend, AI, payments, and data viz.",
            MeansLabel = "What that means for your team — I can independently:",
            MetaText = "Built and operated solo",
            LiveUrl = "https://www.jambalam.com",
            Images = new()
            {
                new ProjectImage { File = "homepage.png", Caption = "Jambalam homepage", SortOrder = 0 },
                new ProjectImage { File = "host-song-imports.png", Caption = "AI-powered song import (Azure AI + GPT-4o-mini)", SortOrder = 1 },
                new ProjectImage { File = "trends-page.png", Caption = "Trends & data visualizations", SortOrder = 2 },
                new ProjectImage { File = "find-a-jam.png", Caption = "Find a jam — session map & discovery", SortOrder = 3 },
            },
            Points = new()
            {
                new ProjectPoint { Title = "Architect extensible systems", Body = "Built on a custom nopCommerce plugin — extending the platform through its plugin model without modifying core (open/closed principle), with clean OOP, dependency injection, and interface-driven design.", SortOrder = 0 },
                new ProjectPoint { Title = "Integrate AI into real products", Body = "Built Azure AI + GPT-4o-mini into the platform for intelligent parsing — not a demo, a working feature.", SortOrder = 1 },
                new ProjectPoint { Title = "Own the full stack", Body = "ASP.NET Core API, React frontend, and SQL Server via EF Core — designed and delivered end to end, solo.", SortOrder = 2 },
                new ProjectPoint { Title = "Handle the hard stuff", Body = "Stripe subscriptions, real-time Web Audio pitch detection, and interactive data visualization (D3 / ECharts / Chart.js).", SortOrder = 3 },
            },
        };

        var skillGroups = new List<SkillGroup>
        {
            new()
            {
                Label = "Backend", SortOrder = 0,
                Skills = new()
                {
                    new Skill { Name = "C#", SortOrder = 0 },
                    new Skill { Name = "ASP.NET Core MVC / Web API", SortOrder = 1 },
                    new Skill { Name = "EF Core / linq2db", SortOrder = 2 },
                    new Skill { Name = "SQL Server / SQLite", SortOrder = 3 },
                    new Skill { Name = "REST APIs", SortOrder = 4 },
                },
            },
            new()
            {
                Label = "Frontend", SortOrder = 1,
                Skills = new()
                {
                    new Skill { Name = "React", SortOrder = 0 },
                    new Skill { Name = "JavaScript", SortOrder = 1 },
                    new Skill { Name = "Razor", SortOrder = 2 },
                    new Skill { Name = "HTML / CSS", SortOrder = 3 },
                },
            },
            new()
            {
                Label = "AI & Cloud", SortOrder = 2,
                Skills = new()
                {
                    new Skill { Name = "Azure AI", SortOrder = 0 },
                    new Skill { Name = "GPT-4o-mini integration", SortOrder = 1 },
                    new Skill { Name = "Azure App Service", SortOrder = 2 },
                    new Skill { Name = "Claude Code", SortOrder = 3 },
                    new Skill { Name = "CodeRabbit (AI code review)", SortOrder = 4 },
                },
            },
            new()
            {
                Label = "Platforms & tools", SortOrder = 3,
                Skills = new()
                {
                    new Skill { Name = "nopCommerce (plugins)", SortOrder = 0 },
                    new Skill { Name = "Stripe", SortOrder = 1 },
                    new Skill { Name = "Git", SortOrder = 2 },
                },
            },
        };

        db.Projects.Add(jambalam);
        db.SkillGroups.AddRange(skillGroups);
        db.SaveChanges();
    }
}
