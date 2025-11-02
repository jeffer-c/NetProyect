using StackExchange.Redis;

namespace NetProyect.Infrastructure.Redis;

public sealed class RedisConnection
{
    private static Lazy<ConnectionMultiplexer> _lazy = null!;
    public static void Configure(string configuration)
        => _lazy = new Lazy<ConnectionMultiplexer>(() => ConnectionMultiplexer.Connect(configuration));
    public static ConnectionMultiplexer Instance => _lazy.Value;
}