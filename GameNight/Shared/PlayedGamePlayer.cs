namespace GameNight.Shared;

public class PlayedGamePlayer
{
    public Guid Id { get; set; }
    public string Name { get; set; } = "Unknown";
    public bool IsWinner { get; set; }
}