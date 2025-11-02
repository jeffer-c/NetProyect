using Serilog;

namespace NetProyect.Api.Config;
public static class SerilogConfig
{
    public static void UseSerilog(WebApplicationBuilder builder)
    {
        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(builder.Configuration)
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .CreateLogger();

        builder.Host.UseSerilog();
    }
}