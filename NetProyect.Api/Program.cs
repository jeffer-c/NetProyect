using Microsoft.EntityFrameworkCore;
using NetProyect.Api.Config;
using NetProyect.Api.Hosted;
using NetProyect.Api.Services;
using NetProyect.Application.Interfaces;
using NetProyect.Application.Services;
using NetProyect.Infrastructure.Http;
using NetProyect.Infrastructure.Persistence;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Serilog
SerilogConfig.UseSerilog(builder);

// EF Core
builder.Services.AddDbContext<NetProyectDbContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Repos + UoW
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Services
builder.Services.AddScoped<IImportService, ImportService>();
builder.Services.AddScoped<IJsonExportService, JsonExportService>();

// Redis singleton
RedisConfig.Configure(builder.Configuration);

// HttpClient + Polly (inyecta programáticamente el API Client con BaseAddress)
builder.Services.AddHttpClient<IExternalApiClient, ExternalApiClient>(http =>
{
    var baseUrl = builder.Configuration["ForbesApi:BaseUrl"]!;
    http.BaseAddress = new Uri(baseUrl);
})
.AddPolicyHandler(PollyConfig.GetRetryPolicy())
.AddPolicyHandler(PollyConfig.GetCircuitBreaker());

// Controllers o Minimal APIs
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Hosted service de arranque
builder.Services.AddHostedService<StartupImportHostedService>();

var app = builder.Build();

app.UseSerilogRequestLogging();
app.UseStaticFiles(); // sirve wwwroot/data/forbes.json via Kestrel
app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.Run();