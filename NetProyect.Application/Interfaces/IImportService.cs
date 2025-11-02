using NetProyect.Domain.Common;

namespace NetProyect.Application.Interfaces;

public interface IImportService
{
    Task<OperationResult<int>> ImportForbesAsync(CancellationToken ct);
}