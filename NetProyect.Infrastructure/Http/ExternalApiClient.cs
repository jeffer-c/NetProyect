using System.Net.Http.Json;
using NetProyect.Application.Dtos;
using NetProyect.Application.Interfaces;

namespace NetProyect.Infrastructure.Http;

public class ExternalApiClient : IExternalApiClient
{
    private readonly HttpClient _http;
    public ExternalApiClient(HttpClient http) => _http = http;

    public async Task<IReadOnlyList<ForbesListDto>> GetForbesListAsync(CancellationToken ct)
        => await _http.GetFromJsonAsync<IReadOnlyList<ForbesListDto>>("list", ct)
           ?? Array.Empty<ForbesListDto>();

    public Task<ProfileDto?> GetProfileAsync(string uri, CancellationToken ct)
        => _http.GetFromJsonAsync<ProfileDto>($"profile/{uri}", ct);
}