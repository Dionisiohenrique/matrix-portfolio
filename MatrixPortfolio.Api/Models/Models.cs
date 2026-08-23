namespace MatrixPortfolio.Api.Models;

public class Project
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string? ImageUrl { get; set; }
    public string? RepoUrl { get; set; }
    public string? LiveUrl { get; set; }
    public string TagsCsv { get; set; } = string.Empty; // "C#,Angular,Postgres"
    public bool IsPublished { get; set; } = true;
    public int DisplayOrder { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class Skill
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    /// <summary>0-100 proficiency shown as a loading bar.</summary>
    public int Level { get; set; }
    public string Category { get; set; } = "General"; // Backend / Frontend / Database / DevOps
    public int DisplayOrder { get; set; }
}

public class ProfileEntry
{
    public int Id { get; set; }
    public string Key { get; set; } = string.Empty; // "name", "headline", "about", "email", "github", "linkedin"
    public string Value { get; set; } = string.Empty;
}

public class ContactMessage
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Body { get; set; } = string.Empty;
    public bool IsRead { get; set; }
    public DateTime CreatedAt { get; set; }
}
