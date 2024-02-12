using GameNight.Server;
using GameNight.Server.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace GameNight.Cli;
internal class MigrateCommand
{
    public void Invoke()
    {
        var serviceProvider = BuildDI();

        using var context = serviceProvider.GetRequiredService<GameContext>();

        context.Database.Migrate();
    }

    private static IServiceProvider BuildDI() =>
        Host.CreateApplicationBuilder()
            .AddSubfolderSettings()
            .AddDatabase(ServiceLifetime.Singleton)
            .Build()
            .Services;
}
