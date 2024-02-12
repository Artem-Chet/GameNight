using System.CommandLine;

namespace GameNight.Cli;

internal class Program
{
    static async Task<int> Main(string[] args)
    {
        var rootCommand = new RootCommand("Game night cli");
        var migrateCommand = new Command("migrate", "Run all pending database migrations");
        rootCommand.Add(migrateCommand);
        migrateCommand.SetHandler(() => (new MigrateCommand()).Invoke());

        return await rootCommand.InvokeAsync(args);
    }
}
