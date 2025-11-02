using NetProyect.Infrastructure.Redis;

namespace NetProyect.Api.Config;
public static class RedisConfig
{
    public static void Configure(IConfiguration cfg)
    {
        var conf = cfg.GetSection("Redis:Configuration").Value!;
        RedisConnection.Configure(conf); // singleton global
    }
}