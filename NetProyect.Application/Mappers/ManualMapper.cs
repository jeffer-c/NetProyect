using NetProyect.Application.Dtos;
using NetProyect.Domain.Entities;

namespace NetProyect.Application.Mappers;

public static class ManualMapper
{
    public static (ForbesList list, Industry industry, Profile profile, NetWorth worth)
        MapForbes(ForbesListDto dto, ProfileDto? p)
    {
        var industry = new Industry { Name = dto.industry ?? "Unknown" };

        var profile = new Profile
        {
            PersonName = p?.personName ?? dto.personName ?? "Unknown",
            LastName = p?.lastName,
            Gender = p?.gender,
            CountryOfCitizenship = p?.countryOfCitizenship,
            Source = p?.source,
            SquareImage = p?.squareImage,
            ImageExists = p?.imageExists ?? false,
            BirthDate = DateOnly.TryParse(p?.birthDate, out var d) ? d : null
        };

        var worth = new NetWorth
        {
            FinalWorth = dto.finalWorth,
            Currency = "USD",
            Formatted = dto.finalWorth.HasValue ? $"{dto.finalWorth:0.##}B" : null
        };

        var list = new ForbesList
        {
            Uri = dto.uri,
            Rank = dto.rank
        };

        return (list, industry, profile, worth);
    }
}