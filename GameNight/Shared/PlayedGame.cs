namespace GameNight.Shared;

public record PlayedGame
{
    public Guid Id { get; set; }
    public string GameName { get; set; } = "Unknown";
    public DateTime? StartedAtUtc { get; set; }
    public int DurationMinutes { get; set; }
    public List<PlayedGamePlayer> Players { get; set; } = new();
}