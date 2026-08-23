using MatrixPortfolio.Api.Data;
using MatrixPortfolio.Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MatrixPortfolio.Api.Controllers;

[ApiController]
[Route("api/projects")]
public class ProjectsController(AppDbContext db) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] bool all = false) =>
        Ok(await db.Projects
            .Where(p => all || p.IsPublished)
            .OrderBy(p => p.DisplayOrder)
            .ToListAsync());

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetOne(int id)
    {
        var p = await db.Projects.FindAsync(id);
        return p is null ? NotFound() : Ok(p);
    }

    [HttpPost, Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create(Project p)
    {
        p.Id = 0;
        p.CreatedAt = DateTime.UtcNow;
        db.Projects.Add(p);
        await db.SaveChangesAsync();
        return CreatedAtAction(nameof(GetOne), new { id = p.Id }, p);
    }

    [HttpPut("{id:int}"), Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update(int id, Project input)
    {
        var p = await db.Projects.FindAsync(id);
        if (p is null) return NotFound();
        db.Entry(p).CurrentValues.SetValues(input);
        await db.SaveChangesAsync();
        return Ok(p);
    }

    [HttpDelete("{id:int}"), Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        var n = await db.Projects.Where(p => p.Id == id).ExecuteDeleteAsync();
        return n == 0 ? NotFound() : NoContent();
    }
}

[ApiController]
[Route("api/skills")]
public class SkillsController(AppDbContext db) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get() =>
        Ok(await db.Skills.OrderBy(s => s.DisplayOrder).ThenBy(s => s.Category).ToListAsync());

    [HttpPost, Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create(Skill s) { db.Skills.Add(s); await db.SaveChangesAsync(); return Ok(s); }

    [HttpPut("{id:int}"), Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update(int id, Skill input)
    {
        var s = await db.Skills.FindAsync(id);
        if (s is null) return NotFound();
        db.Entry(s).CurrentValues.SetValues(input);
        await db.SaveChangesAsync();
        return Ok(s);
    }

    [HttpDelete("{id:int}"), Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        var n = await db.Skills.Where(s => s.Id == id).ExecuteDeleteAsync();
        return n == 0 ? NotFound() : NoContent();
    }
}

[ApiController]
[Route("api/profile")]
public class ProfileController(AppDbContext db) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get() =>
        Ok(await db.ProfileEntries.ToDictionaryAsync(e => e.Key, e => e.Value));

    [HttpPut("{key}"), Authorize(Roles = "Admin")]
    public async Task<IActionResult> Set(string key, [FromBody] ProfileValue value)
    {
        var entry = await db.ProfileEntries.FirstOrDefaultAsync(e => e.Key == key);
        if (entry is null) db.ProfileEntries.Add(new ProfileEntry { Key = key, Value = value.Value });
        else entry.Value = value.Value;
        await db.SaveChangesAsync();
        return Ok(new { key, value.Value });
    }
}

public record ProfileValue(string Value);

[ApiController]
[Route("api/messages")]
public class MessagesController(AppDbContext db) : ControllerBase
{
    [HttpPost] // public: contact form
    public async Task<IActionResult> Send(ContactMessage m)
    {
        m.Id = 0; m.CreatedAt = DateTime.UtcNow; m.IsRead = false;
        db.Messages.Add(m);
        await db.SaveChangesAsync();
        return Ok(new { received = true });
    }

    [HttpGet, Authorize(Roles = "Admin")]
    public async Task<IActionResult> Get() =>
        Ok(await db.Messages.OrderByDescending(m => m.CreatedAt).ToListAsync());

    [HttpPut("{id:int}/read"), Authorize(Roles = "Admin")]
    public async Task<IActionResult> MarkRead(int id)
    {
        await db.Messages.Where(m => m.Id == id).ExecuteUpdateAsync(u => u.SetProperty(m => m.IsRead, true));
        return NoContent();
    }

    [HttpDelete("{id:int}"), Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        var n = await db.Messages.Where(m => m.Id == id).ExecuteDeleteAsync();
        return n == 0 ? NotFound() : NoContent();
    }
}
