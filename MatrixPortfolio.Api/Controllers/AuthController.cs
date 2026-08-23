using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace MatrixPortfolio.Api.Controllers;

public record LoginRequest(string Username, string Password);

[ApiController]
[Route("api/auth")]
public class AuthController(IConfiguration config) : ControllerBase
{
    // Single-admin portfolio: credentials come from env vars (ADMIN_USERNAME / ADMIN_PASSWORD).
    // For production-grade apps, swap for ASP.NET Core Identity + hashed passwords in the DB.
    private readonly string _user = config["Admin:Username"] ?? "admin";
    private readonly string _pass = config["Admin:Password"] ?? "ChangeMe!123";

    [HttpPost("login")]
    public IActionResult Login(LoginRequest req)
    {
        if (req.Username != _user || req.Password != _pass)
            return Unauthorized(new { message = "Wrong username or password. Wake up, Neo..." });

        var key = new SymmetricSecurityKey(
            System.Text.Encoding.UTF8.GetBytes(config["Jwt:Key"]
                ?? throw new InvalidOperationException("Jwt:Key not configured")));

        var claims = new List<Claim>
        {
            new(ClaimTypes.Name, req.Username),
            new(ClaimTypes.Role, "Admin")
        };

        var token = new JwtSecurityToken(
            issuer: config["Jwt:Issuer"] ?? "matrix-portfolio",
            audience: config["Jwt:Audience"] ?? "matrix-portfolio",
            claims: claims,
            expires: DateTime.UtcNow.AddHours(8),
            signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256));

        return Ok(new
        {
            token = new JwtSecurityTokenHandler().WriteToken(token),
            expiresInHours = 8
        });
    }
}
