using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameNight.Shared;

public class PlayedGame
{
    public Guid Id { get; set; }
    public string GameName { get; set; } = "Unknown";
    public DateTime StartedAtUtc { get; set; }
    public int DurationMinutes { get; set; }
    public List<PlayedGamePlayer> Players { get; set; } = new();
}