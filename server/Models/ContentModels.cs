namespace Server.Models;

// The featured project (Jambalam) and its related images/points.
public class Project
{
    public int Id { get; set; }
    public string Title { get; set; } = "";
    public string Lead { get; set; } = "";
    public string MeansLabel { get; set; } = "";
    public string MetaText { get; set; } = "";
    public string LiveUrl { get; set; } = "";

    public List<ProjectImage> Images { get; set; } = new();
    public List<ProjectPoint> Points { get; set; } = new();
}

// Screenshots. Ordered by SortOrder; the first one is treated as the hero image.
public class ProjectImage
{
    public int Id { get; set; }
    public string File { get; set; } = "";
    public string Caption { get; set; } = "";
    public int SortOrder { get; set; }

    public int ProjectId { get; set; }
    public Project? Project { get; set; }
}

// "What that means for your team" bullet points.
public class ProjectPoint
{
    public int Id { get; set; }
    public string Title { get; set; } = "";
    public string Body { get; set; } = "";
    public int SortOrder { get; set; }

    public int ProjectId { get; set; }
    public Project? Project { get; set; }
}

// Skills, grouped (Backend / Frontend / etc.).
public class SkillGroup
{
    public int Id { get; set; }
    public string Label { get; set; } = "";
    public int SortOrder { get; set; }

    public List<Skill> Skills { get; set; } = new();
}

public class Skill
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public int SortOrder { get; set; }

    public int SkillGroupId { get; set; }
    public SkillGroup? SkillGroup { get; set; }
}
