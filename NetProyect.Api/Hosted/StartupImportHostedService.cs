using NetProyect.Application.Interfaces;
using Serilog;

namespace NetProyect.Api.Hosted;

public class StartupImportHostedService : IHostedService
{
    private readonly IImportService _import;
    public StartupImportHostedService(IImportService import) => _import = import;

    public async Task StartAsync(CancellationToken ct)
    {
        try
        {
            var result = await _import.ImportForbesAsync(ct);
            if (!result.Success) Log.Error("Import error: {Err}", result.Error);
            else Log.Information("Imported {Count} entries", result.Value);
        }
        catch (Exception ex) { Log.Error(ex, "Import failed"); }
    }
    public Task StopAsync(CancellationToken ct) => Task.CompletedTask;
}