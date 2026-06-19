using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Server.Data;

namespace Server.Controllers;

// Serves the page content (project + skills) from the database.
// GET /api/content
[ApiController]
[Route("api/content")]
public class ContentController : ControllerBase
{
    private readonly AppDbContext _db;

    public ContentController(AppDbContext db) => _db = db;

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var project = await _db.Projects
            .OrderBy(p => p.Id)
            .Include(p => p.Images)
            .Include(p => p.Points)
            .Select(p => new
            {
                p.Title,
                p.Lead,
                p.MeansLabel,
                p.MetaText,
                p.LiveUrl,
                Images = p.Images.OrderBy(i => i.SortOrder).Select(i => new { i.File, i.Caption }),
                Points = p.Points.OrderBy(pt => pt.SortOrder).Select(pt => new { pt.Title, pt.Body }),
            })
            .FirstOrDefaultAsync();

        var skillGroups = await _db.SkillGroups
            .OrderBy(g => g.SortOrder)
            .Select(g => new
            {
                g.Label,
                Skills = g.Skills.OrderBy(s => s.SortOrder).Select(s => s.Name),
            })
            .ToListAsync();

        return Ok(new { project, skillGroups });
    }
}
