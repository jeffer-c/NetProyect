using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetProyect.Application.Interfaces;
using NetProyect.Application.Services;
using NetProyect.CrossCutting.Constants;
using NetProyect.Infrastructure.Http;
using NetProyect.Infrastructure.Persistence;
using NetProyect.Infrastructure.Redis;

namespace NetProyect.CrossCutting.Extensions;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// DbContext, repositorios genéricos y Unit of Work.
    /// </summary>
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration cfg)
    {
        var cs = cfg.GetConnectionString(AppConstants.ConnectionStrings.Default)
                 ?? throw new InvalidOperationException("Falta la cadena de conexión 'DefaultConnection'.");

        services.AddDbContext<NetProyectDbContext>(opt => opt.UseSqlServer(cs));

        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }

    /// <summary>
    /// Clientes HTTP externos con IHttpClientFactory (Forbes API).
    /// Añade Polly en Program.cs con AddPolicyHandler si lo deseas.
    /// </summary>
    public static IServiceCollection AddExternalClients(this IServiceCollection services, IConfiguration cfg)
    {
        var baseUrl = cfg[AppConstants.ConfigSections.ForbesApiBaseUrl]
                      ?? "https://forbes-api.vercel.app/";

        services.AddHttpClient<IExternalApiClient, ExternalApiClient>(client =>
        {
            client.BaseAddress = new Uri(baseUrl);
            client.Timeout = TimeSpan.FromSeconds(30);
        });

        return services;
    }

    /// <summary>
    /// Casos de uso / servicios de la capa Application.
    /// (IJsonExportService se registra en la capa Api porque su implementación vive allí.)
    /// </summary>
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IImportService, ImportService>();
        return services;
    }

    /// <summary>
    /// Configura Redis como singleton (una sola conexión global).
    /// Expone ConnectionMultiplexer e IDatabase por DI si se requiere.
    /// </summary>
    public static IServiceCollection AddRedisSingleton(this IServiceCollection services, IConfiguration cfg)
    {
        var redisCfg = cfg[AppConstants.ConfigSections.RedisConfiguration]
                       ?? throw new InvalidOperationException("Falta 'Redis:Configuration' en appsettings.");

        RedisConnection.Configure(redisCfg);                 // inicializa singleton
        services.AddSingleton(provider => RedisConnection.Instance);
        services.AddSingleton(sp => RedisConnection.Instance.GetDatabase());

        return services;
    }

    /// <summary>
    /// Registro “todo en uno” recomendado para Program.cs.
    /// </summary>
    public static IServiceCollection AddNetProyectCore(this IServiceCollection services, IConfiguration cfg)
        => services
            .AddPersistence(cfg)
            .AddExternalClients(cfg)
            .AddApplicationServices()
            .AddRedisSingleton(cfg);
}