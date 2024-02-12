using GameNight.Server.Database;
using Microsoft.EntityFrameworkCore;

namespace GameNight.Server;

public static class HostExtensions
{
    public static TBuilder AddSubfolderSettings<TBuilder>(this TBuilder builder) where TBuilder : IHostApplicationBuilder
    {
        // Using non-standard appsettings location to allow K8s hot reload. K8s requires config maps to map to an entire directory and not just select files.
        builder.Configuration.AddJsonFile("Settings/Config/appsettings.json", optional: false, reloadOnChange: true);
        builder.Configuration.AddJsonFile($"Settings/Config/appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true);
        builder.Configuration.AddJsonFile("Settings/Secrets/appsettings.secrets.json", optional: true, reloadOnChange: true);

        return builder;
    }

    public static TBuilder AddDatabase<TBuilder>(this TBuilder builder, ServiceLifetime serviceLifetime = ServiceLifetime.Scoped) where TBuilder : IHostApplicationBuilder
    {
        builder.Services.AddDbContext<GameContext>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString("GameContext"))
                   .UseSnakeCaseNamingConvention(),
                   serviceLifetime
            );

        return builder;
    }
}
