using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MatrixPortfolio.Api.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Body = table.Column<string>(type: "text", nullable: false),
                    IsRead = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProfileEntries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Key = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfileEntries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    ImageUrl = table.Column<string>(type: "text", nullable: true),
                    RepoUrl = table.Column<string>(type: "text", nullable: true),
                    LiveUrl = table.Column<string>(type: "text", nullable: true),
                    TagsCsv = table.Column<string>(type: "text", nullable: false),
                    IsPublished = table.Column<bool>(type: "boolean", nullable: false),
                    DisplayOrder = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Skills",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Level = table.Column<int>(type: "integer", nullable: false),
                    Category = table.Column<string>(type: "text", nullable: false),
                    DisplayOrder = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skills", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "ProfileEntries",
                columns: new[] { "Id", "Key", "Value" },
                values: new object[,]
                {
                    { 1, "name", "Jessica Lopes" },
                    { 2, "headline", "Middle-level C# Developer // waking up from the Matrix" },
                    { 3, "about", "I build APIs and web apps with C#, .NET and Angular. This portfolio itself is a fullstack project: ASP.NET Core 8 + EF Core + PostgreSQL on the back, Angular on the front, deployed for free on Render + Neon + Netlify." },
                    { 4, "email", "jessicas2mirelly1704@gmail.com" },
                    { 5, "github", "https://github.com/your-user" },
                    { 6, "linkedin", "" }
                });

            migrationBuilder.InsertData(
                table: "Projects",
                columns: new[] { "Id", "CreatedAt", "Description", "DisplayOrder", "ImageUrl", "IsPublished", "LiveUrl", "RepoUrl", "TagsCsv", "Title" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 8, 23, 22, 0, 24, 112, DateTimeKind.Utc).AddTicks(5615), "Real-time monitoring dashboard built with ASP.NET Core + SignalR and Angular. Streams live metrics over WebSockets with a terminal-inspired UI.", 1, null, true, "", "https://github.com/your-user/nebuchadnezzar-dashboard", "C#,.NET 8,Angular,SignalR", "Nebuchadnezzar Dashboard" },
                    { 2, new DateTime(2026, 8, 23, 22, 0, 24, 113, DateTimeKind.Utc).AddTicks(509), "JWT authentication microservice with refresh-token rotation, rate limiting and PostgreSQL persistence. Deployed on Docker.", 2, null, true, null, "https://github.com/your-user/zion-auth", "C#,JWT,PostgreSQL,Docker", "Zion Auth Gateway" },
                    { 3, new DateTime(2026, 8, 23, 22, 0, 24, 113, DateTimeKind.Utc).AddTicks(518), "Online code execution sandbox: Angular frontend queues jobs to a .NET worker that compiles/runs snippets in isolated processes.", 3, null, true, null, "https://github.com/your-user/construct-runner", ".NET,Angular,RabbitMQ", "Construct Code Runner" }
                });

            migrationBuilder.InsertData(
                table: "Skills",
                columns: new[] { "Id", "Category", "DisplayOrder", "Level", "Name" },
                values: new object[,]
                {
                    { 1, "Backend", 0, 85, "C# / .NET" },
                    { 2, "Backend", 0, 80, "ASP.NET Core Web API" },
                    { 3, "Backend", 0, 78, "Entity Framework Core" },
                    { 4, "Frontend", 0, 72, "Angular" },
                    { 5, "Frontend", 0, 70, "TypeScript" },
                    { 6, "Database", 0, 65, "PostgreSQL" },
                    { 7, "DevOps", 0, 55, "Docker" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropTable(
                name: "ProfileEntries");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropTable(
                name: "Skills");
        }
    }
}
