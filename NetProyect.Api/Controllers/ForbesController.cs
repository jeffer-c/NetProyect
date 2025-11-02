using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetProyect.Infrastructure.Persistence;

namespace NetProyect.Api.Controllers;

[ApiController]
[Route("api/forbes")]
public class ForbesController : ControllerBase
{
    private readonly NetProyectDbContext _ctx;
    public ForbesController(NetProyectDbContext ctx) => _ctx = ctx;

    [HttpGet]
    public async Task<IActionResult> Get(CancellationToken ct)
    {
        var data = await _ctx.ForbesLists
            .AsNoTracking()
            .Include(x => x.Industry).Include(x => x.Profile).Include(x => x.NetWorth)
            .ToListAsync(ct);
        return Ok(data);
    }

    // Sirves el JSON estático con Kestrel en /data/forbes.json (UseStaticFiles)
}