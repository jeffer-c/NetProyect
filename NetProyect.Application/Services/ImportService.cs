using NetProyect.Application.Interfaces;
using NetProyect.Application.Mappers;
using NetProyect.Domain.Common;
using NetProyect.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace NetProyect.Application.Services;

public class ImportService : IImportService
{
    private readonly IExternalApiClient _client;
    private readonly IGenericRepository<ForbesList> _forbesRepo;
    private readonly IGenericRepository<Industry> _industryRepo;
    private readonly IGenericRepository<Profile> _profileRepo;
    private readonly IGenericRepository<NetWorth> _worthRepo;
    private readonly IUnitOfWork _uow;
    private readonly IJsonExportService _export;

    public ImportService(
        IExternalApiClient client,
        IGenericRepository<ForbesList> forbesRepo,
        IGenericRepository<Industry> industryRepo,
        IGenericRepository<Profile> profileRepo,
        IGenericRepository<NetWorth> worthRepo,
        IUnitOfWork uow,
        IJsonExportService export)
    {
        _client = client; _forbesRepo = forbesRepo; _industryRepo = industryRepo;
        _profileRepo = profileRepo; _worthRepo = worthRepo; _uow = uow; _export = export;
    }

    public async Task<OperationResult<int>> ImportForbesAsync(CancellationToken ct)
    {
        var list = await _client.GetForbesListAsync(ct);
        var count = 0;

        foreach (var dto in list)
        {
            var profileDto = await _client.GetProfileAsync(dto.uri, ct);

            var (entry, industry, profile, worth) = ManualMapper.MapForbes(dto, profileDto);

            // Reutiliza Industry por nombre si ya existe (búsqueda simple)
            var existingIndustry = _industryRepo.Query().FirstOrDefault(i => i.Name == industry.Name);
            if (existingIndustry is null)
            {
                await _industryRepo.AddAsync(industry, ct);
                existingIndustry = industry;
            }

            await _profileRepo.AddAsync(profile, ct);
            await _worthRepo.AddAsync(worth, ct);
            await _uow.SaveChangesAsync(ct);

            entry.IndustryId = existingIndustry.Id;
            entry.ProfileId = profile.Id;
            entry.NetWorthId = worth.Id;

            await _forbesRepo.AddAsync(entry, ct);
            count++;

            // Puedes hacer batching/SaveChanges por lotes si la lista es grande
        }

        await _uow.SaveChangesAsync(ct);

        // Exporta JSON y lo cachea en Redis
        var jsonPath = await _export.ExportForbesToJsonAsync(ct);
        // json también se guarda en Redis dentro del ExportService

        return OperationResult<int>.Ok(count);
    }
}