using NetProyect.Application.Dtos;

namespace NetProyect.Application.Interfaces;

public interface IExternalApiClient
{
    Task<IReadOnlyList<ForbesListDto>> GetForbesListAsync(CancellationToken ct);
    Task<ProfileDto?> GetProfileAsync(string uri, CancellationToken ct);
}