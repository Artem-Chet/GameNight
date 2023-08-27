using GameNight.Server.Database;
using GameNight.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GameNight.Server.PlayedGames;

[Route("api/[controller]")]
[ApiController]
public class PlayedGamesController : ControllerBase
{
    public PlayedGamesController(GameContext gameContext)
    {
        GameContext = gameContext;
    }

    private GameContext GameContext { get; }

    [HttpGet]
    public List<PlayedGame> Index()
    {
        return GameContext.PlayedGames
             .Include(x => x.Players)
             .ToList();

    }

    [HttpPost]
    public async Task<ActionResult<PlayedGame>> AddPlayedGame(PlayedGame game)
    {
        GameContext.PlayedGames.Add(game);
        await GameContext.SaveChangesAsync();
        return game;
    }

}