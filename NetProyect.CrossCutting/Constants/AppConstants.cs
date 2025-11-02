namespace NetProyect.CrossCutting.Constants;

public static class AppConstants
{
    public static class ConnectionStrings
    {
        public const string Default = "DefaultConnection";
    }

    public static class ConfigSections
    {
        public const string ForbesApiBaseUrl = "ForbesApi:BaseUrl";
        public const string RedisConfiguration = "Redis:Configuration";
    }

    public static class CacheKeys
    {
        public const string ForbesJson = "forbes:json";
    }

    public static class Paths
    {
        public const string WebRootDataFolder = "data";
        public const string ForbesJsonFileName = "forbes.json";
        public static string ForbesJsonRelative => $"{WebRootDataFolder}/{ForbesJsonFileName}";
    }
}