using GameNight.Server.Users;
using GameNight.Shared;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GameNight.Server.Database;

public class UserContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
{
    public DbSet<PlayedGame> PlayedGames { get; set; }

    public UserContext(DbContextOptions<UserContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.HasDefaultSchema("users");
    }
}