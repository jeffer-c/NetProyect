using FluentAssertions;
using NetProyect.Application.Dtos;
using NetProyect.Application.Interfaces;
using NetProyect.Application.Services;
using NetProyect.Domain.Entities;
using NSubstitute;
using Xunit;

public class ImportServiceTests
{
    [Fact]
    public async Task ImportForbesAsync_SavesEntries()
    {
        var client = Substitute.For<IExternalApiClient>();
        client.GetForbesListAsync(Arg.Any<CancellationToken>())
            .Returns([new ForbesListDto { uri = "elon-musk", rank = 1, industry = "Automotive", finalWorth = 200 }]);
        client.GetProfileAsync("elon-musk", Arg.Any<CancellationToken>())
            .Returns(new ProfileDto { personName = "Elon", countryOfCitizenship = "US" });

        var repoF = Substitute.For<IGenericRepository<ForbesList>>();
        var repoI = Substitute.For<IGenericRepository<Industry>>();
        var repoP = Substitute.For<IGenericRepository<Profile>>();
        var repoW = Substitute.For<IGenericRepository<NetWorth>>();
        var uow = Substitute.For<IUnitOfWork>();
        var export = Substitute.For<IJsonExportService>();

        var sut = new ImportService(client, repoF, repoI, repoP, repoW, uow, export);
        var result = await sut.ImportForbesAsync(default);

        result.Success.Should().BeTrue();
        await repoF.Received().AddAsync(Arg.Any<ForbesList>(), Arg.Any<CancellationToken>());
        await uow.Received().SaveChangesAsync(Arg.Any<CancellationToken>());
    }
}