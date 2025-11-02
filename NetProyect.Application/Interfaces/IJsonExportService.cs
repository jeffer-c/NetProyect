namespace NetProyect.Application.Interfaces;

public interface IJsonExportService
{
    Task<string> ExportForbesToJsonAsync(CancellationToken ct); // devuelve ruta del archivo
    Task CacheForbesJsonAsync(string json, CancellationToken ct);
}