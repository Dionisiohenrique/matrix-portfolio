using System.Text;
using MatrixPortfolio.Api.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();

// Neon/Heroku-style URIs ("postgresql://user:pass@host/db?sslmode=require")
// are not accepted by Npgsql directly — convert to keyword format.
static string NormalizeConnectionString(string raw)
{
    if (!raw.StartsWith("postgres://", StringComparison.OrdinalIgnoreCase) &&
        !raw.StartsWith("postgresql://", StringComparison.OrdinalIgnoreCase))
        return raw;

    var uri = new Uri(raw.Replace("postgres://", "http://").Replace("postgresql://", "http://"));
    var parts = System.Web.HttpUtility.ParseQueryString(uri.Query);
    var sb = new StringBuilder();
    sb.Append($"Host={uri.Host};");
    if (uri.Port > 0) sb.Append($"Port={uri.Port};");
    sb.Append($"Database={uri.AbsolutePath.Trim('/')};");
    sb.Append($"Username={Uri.UnescapeDataString(uri.UserInfo.Split(':')[0])};");
    var pwd = uri.UserInfo.Split(':')[1..];
    sb.Append($"Password={Uri.UnescapeDataString(string.Join(':', pwd))};");
    if (parts["sslmode"] == "require" || uri.Host.Contains("neon")) sb.Append("SSL Mode=Require;");
    return sb.ToString();
}

builder.Services.AddDbContext<AppDbContext>(o =>
    o.UseNpgsql(NormalizeConnectionString(builder.Configuration.GetConnectionString("Default") ?? "")));

builder.Services.AddCors(o => o.AddPolicy("front", p => p
    .WithOrigins(
        "http://localhost:4200",
        builder.Configuration["Frontend:Url"] ?? "http://localhost:4200")
    .AllowAnyHeader()
    .AllowAnyMethod()));

var jwtKey = builder.Configuration["Jwt:Key"];
if (string.IsNullOrEmpty(jwtKey))
    throw new InvalidOperationException("Set Jwt:Key (env: Jwt__Key, min 32 chars).");

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(o => o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"] ?? "matrix-portfolio",
        ValidateAudience = true,
        ValidAudience = builder.Configuration["Jwt:Audience"] ?? "matrix-portfolio",
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),
        ClockSkew = TimeSpan.FromMinutes(1)
    });
builder.Services.AddAuthorization();

var app = builder.Build();

// Apply migrations + seed automatically at startup (convenient for free hosts).
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseDefaultFiles(); // serves index.html if we ever host the Angular build here
app.UseStaticFiles();

app.UseCors("front");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();
