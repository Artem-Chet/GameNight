using GameNight.Shared;
using Microsoft.AspNetCore.Mvc;

namespace GameNight.Server.PlayedGames;

[Route("api/[controller]")]
[ApiController]
public class PlayedGamesController : ControllerBase
{
    [HttpGet]
    public List<PlayedGame> Index()
    {
        return new List<PlayedGame>()
        {
            new PlayedGame()
            {
                GameName = "Monopoly",
                DurationMinutes = 60,
                StartedAtUtc = new DateTime(2023, 8, 11, 7, 0, 0, DateTimeKind.Utc),
                Players = new()
                {
                    new()
                    {
                        Name = "Artem",
                        IsWinner = true,
                    },
                    new()
                    {
                        Name = "Roman",
                        IsWinner = false,
                    }
                }
            }
        };
    }
/*
    [HttpPost]
    public async Task<ActionResult<PlayedGame>> AddPlayedGame(PlayedGame game)
    {
        try
        {
            if(game == null)
            {
                return BadRequest();
            }
        }
    }
*/
}