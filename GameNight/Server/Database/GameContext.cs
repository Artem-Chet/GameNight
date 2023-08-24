using GameNight.Shared;
using Microsoft.EntityFrameworkCore;

namespace GameNight.Server.Database;

public class GameContext : DbContext
{
    public DbSet<PlayedGame> PlayedGame { get; set; }

    public GameContext(DbContextOptions<GameContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PlayedGame>()
            .HasKey(x => x.Id);

        modelBuilder.Entity<PlayedGamePlayer>()
            .HasKey(x => x.Id);
    }
}