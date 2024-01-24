using GameNight.Server.Auth;
using GameNight.Shared;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GameNight.Server.Database;

public class GameContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
{
    public DbSet<PlayedGame> PlayedGames { get; set; }

    public GameContext(DbContextOptions<GameContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<PlayedGame>()
            .ToTable("played_games")
            .HasKey(x => x.Id);

        modelBuilder.Entity<PlayedGamePlayer>()
            .ToTable("played_game_players")
            .HasKey(x => x.Id);

        modelBuilder.Entity<User>()
            .ToTable("users", "users");

        modelBuilder.Entity<IdentityRole<Guid>>()
            .ToTable("roles", "users");

        modelBuilder.Entity<IdentityUserRole<Guid>>()
            .ToTable("user_roles", "users");

        modelBuilder.Entity<IdentityRoleClaim<Guid>>()
            .ToTable("user_role_claims", "users");

        modelBuilder.Entity<IdentityUserClaim<Guid>>()
            .ToTable("user_claims", "users");

        modelBuilder.Entity<IdentityUserLogin<Guid>>()
            .ToTable("user_logins", "users");

        modelBuilder.Entity<IdentityUserToken<Guid>>()
            .ToTable("user_tokens", "users");
    }
}