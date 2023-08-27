using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameNight.Server.Migrations;

/// <inheritdoc />
public partial class AddPlayedGames : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "played_games",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                game_name = table.Column<string>(type: "text", nullable: false),
                started_at_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                duration_minutes = table.Column<int>(type: "integer", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_played_games", x => x.id);
            });

        migrationBuilder.CreateTable(
            name: "played_game_players",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                name = table.Column<string>(type: "text", nullable: false),
                is_winner = table.Column<bool>(type: "boolean", nullable: false),
                played_game_id = table.Column<Guid>(type: "uuid", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_played_game_players", x => x.id);
                table.ForeignKey(
                    name: "fk_played_game_players_played_games_played_game_id",
                    column: x => x.played_game_id,
                    principalTable: "played_games",
                    principalColumn: "id");
            });

        migrationBuilder.CreateIndex(
            name: "ix_played_game_players_played_game_id",
            table: "played_game_players",
            column: "played_game_id");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "played_game_players");

        migrationBuilder.DropTable(
            name: "played_games");
    }
}