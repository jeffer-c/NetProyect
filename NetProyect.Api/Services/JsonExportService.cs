using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using NetProyect.Application.Interfaces;
using NetProyect.Infrastructure.Persistence;
using NetProyect.Infrastructure.Redis;
using StackExchange.Redis;

namespace NetProyect.Api.Services;

public class JsonExportService : IJsonExportService
{
    private readonly NetProyectDbContext _ctx;
    private readonly IWebHostEnvironment _env;

    public JsonExportService(NetProyectDbContext ctx, IWebHostEnvironment env)
    { _ctx = ctx; _env = env; }

    public async Task<string> ExportForbesToJsonAsync(CancellationToken ct)
    {
        var data = await _ctx.ForbesLists
            .AsNoTracking()
            .Include(x => x.Industry)
            .Include(x => x.Profile)
            .Include(x => x.NetWorth)
            .ToListAsync(ct);

        var json = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });

        var dir = Path.Combine(_env.WebRootPath ?? "wwwroot", "data");
        Directory.CreateDirectory(dir);
        var path = Path.Combine(dir, "forbes.json");
        await File.WriteAllTextAsync(path, json, ct);

        await CacheForbesJsonAsync(json, ct);
        return path;
    }

    public async Task CacheForbesJsonAsync(string json, CancellationToken ct)
    {
        var db = RedisConnection.Instance.GetDatabase();
        // sin concurrencias, un único config/instancia
        await db.StringSetAsync("forbes:json", json);
    }
}