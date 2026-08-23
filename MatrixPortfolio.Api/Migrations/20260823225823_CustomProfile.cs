using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MatrixPortfolio.Api.Migrations
{
    /// <inheritdoc />
    public partial class CustomProfile : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ProfileEntries",
                keyColumn: "Id",
                keyValue: 1,
                column: "Value",
                value: "Henrique Dionisio Ferreira");

            migrationBuilder.UpdateData(
                table: "ProfileEntries",
                keyColumn: "Id",
                keyValue: 3,
                column: "Value",
                value: "I build APIs and web apps with C#, .NET and Angular. This portfolio itself is a fullstack project: ASP.NET Core + EF Core + PostgreSQL on the back, Angular on the front — deployed for free on Render + Neon + Netlify.");

            migrationBuilder.UpdateData(
                table: "ProfileEntries",
                keyColumn: "Id",
                keyValue: 5,
                column: "Value",
                value: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ProfileEntries",
                keyColumn: "Id",
                keyValue: 1,
                column: "Value",
                value: "Jessica Lopes");

            migrationBuilder.UpdateData(
                table: "ProfileEntries",
                keyColumn: "Id",
                keyValue: 3,
                column: "Value",
                value: "I build APIs and web apps with C#, .NET and Angular. This portfolio itself is a fullstack project: ASP.NET Core 8 + EF Core + PostgreSQL on the back, Angular on the front, deployed for free on Render + Neon + Netlify.");

            migrationBuilder.UpdateData(
                table: "ProfileEntries",
                keyColumn: "Id",
                keyValue: 5,
                column: "Value",
                value: "https://github.com/your-user");
        }
    }
}
