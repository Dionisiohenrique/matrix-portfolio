using MatrixPortfolio.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace MatrixPortfolio.Api.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Project> Projects => Set<Project>();
    public DbSet<Skill> Skills => Set<Skill>();
    public DbSet<ProfileEntry> ProfileEntries => Set<ProfileEntry>();
    public DbSet<ContactMessage> Messages => Set<ContactMessage>();

    protected override void OnModelCreating(ModelBuilder b)
    {
        b.Entity<Project>().Property(p => p.CreatedAt).HasDefaultValueSql("now() at time zone 'utc'");
        b.Entity<ContactMessage>().Property(m => m.CreatedAt).HasDefaultValueSql("now() at time zone 'utc'");
        b.Entity<Project>().HasData(
            new Project { Id = 1, Title = "Nebuchadnezzar Dashboard", Description = "Real-time monitoring dashboard built with ASP.NET Core + SignalR and Angular. Streams live metrics over WebSockets with a terminal-inspired UI.", RepoUrl = "https://github.com/your-user/nebuchadnezzar-dashboard", LiveUrl = "", TagsCsv = "C#,.NET 8,Angular,SignalR", DisplayOrder = 1 },
            new Project { Id = 2, Title = "Zion Auth Gateway", Description = "JWT authentication microservice with refresh-token rotation, rate limiting and PostgreSQL persistence. Deployed on Docker.", RepoUrl = "https://github.com/your-user/zion-auth", TagsCsv = "C#,JWT,PostgreSQL,Docker", DisplayOrder = 2 },
            new Project { Id = 3, Title = "Construct Code Runner", Description = "Online code execution sandbox: Angular frontend queues jobs to a .NET worker that compiles/runs snippets in isolated processes.", RepoUrl = "https://github.com/your-user/construct-runner", TagsCsv = ".NET,Angular,RabbitMQ", DisplayOrder = 3 });
        b.Entity<Skill>().HasData(
            new Skill { Id = 1, Name = "C# / .NET", Level = 85, Category = "Backend" },
            new Skill { Id = 2, Name = "ASP.NET Core Web API", Level = 80, Category = "Backend" },
            new Skill { Id = 3, Name = "Entity Framework Core", Level = 78, Category = "Backend" },
            new Skill { Id = 4, Name = "Angular", Level = 72, Category = "Frontend" },
            new Skill { Id = 5, Name = "TypeScript", Level = 70, Category = "Frontend" },
            new Skill { Id = 6, Name = "PostgreSQL", Level = 65, Category = "Database" },
            new Skill { Id = 7, Name = "Docker", Level = 55, Category = "DevOps" });
        b.Entity<ProfileEntry>().HasData(
            new ProfileEntry { Id = 1, Key = "name", Value = "Henrique Dionisio Ferreira" },
            new ProfileEntry { Id = 2, Key = "headline", Value = "Middle-level C# Developer // waking up from the Matrix" },
            new ProfileEntry { Id = 3, Key = "about", Value = "I build APIs and web apps with C#, .NET and Angular. This portfolio itself is a fullstack project: ASP.NET Core + EF Core + PostgreSQL on the back, Angular on the front — deployed for free on Render + Neon + Netlify." },
            new ProfileEntry { Id = 4, Key = "email", Value = "jessicas2mirelly1704@gmail.com" },
            new ProfileEntry { Id = 5, Key = "github", Value = "" },
            new ProfileEntry { Id = 6, Key = "linkedin", Value = "" });
        base.OnModelCreating(b);
    }
}
