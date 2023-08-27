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
            .ToTable("played_games")
            .HasKey(x => x.Id);

        modelBuilder.Entity<PlayedGamePlayer>()
            .ToTable("played_game_players")
            .HasKey(x => x.Id);
    }
}