using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using NetProyect.Application.Interfaces;
using NetProyect.Infrastructure.Http;
using RichardSzalay.MockHttp;
using System.Net;
using System.Text.Json;
using Xunit;

public class ExternalHttpMockTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public ExternalHttpMockTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory.WithWebHostBuilder(builder =>
        {
            builder.ConfigureServices(services =>
            {
                // Reemplaza HttpClient del ExternalApiClient por MockHttp
                var mockHttp = new MockHttpMessageHandler();
                mockHttp.When("*/list").Respond("application/json",
                    JsonSerializer.Serialize(new[] { new { uri = "elon-musk", rank = 1, industry = "Automotive", finalWorth = 200, personName = "Elon" } }));
                mockHttp.When("*/profile/elon-musk").Respond("application/json",
                    JsonSerializer.Serialize(new { personName = "Elon", countryOfCitizenship = "US" }));

                //services.AddHttpClient<IExternalApiClient, NetProyect.Infrastructure.Http.ExternalApiClient>()
                //        .ConfigureHttpMessageHandlerBuilder(b => b.PrimaryHandler = mockHttp);
                services.AddHttpClient<IExternalApiClient, NetProyect.Infrastructure.Http.ExternalApiClient>()
                          .ConfigurePrimaryHttpMessageHandler(() => mockHttp);
            });
        });
    }

    [Fact]
    public async Task Startup_Imports_And_Exposes_Json()
    {
        var client = _factory.CreateClient();
        var resp = await client.GetAsync("/api/forbes");
        resp.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}