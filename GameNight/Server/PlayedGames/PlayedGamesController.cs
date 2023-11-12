using GameNight.Server.Database;
using GameNight.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

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
    [HttpGet("{id}")]
    public async Task<ActionResult<PlayedGame>> GetPlayedGame(Guid id)
    {
          var game = await GameContext.PlayedGames.Where(x => x.Id == id)
             .Include(x => x.Players)
             .FirstOrDefaultAsync();
        if (game is null)
        {
            return NotFound();
        }

        return game;

    }
    [HttpPost]
    public async Task<ActionResult<PlayedGame>> AddPlayedGame(PlayedGame game)
    {
        GameContext.PlayedGames.Add(game);
        await GameContext.SaveChangesAsync();
        return game;
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteGame(Guid id)
    {

        var game = await GameContext.PlayedGames.Include(x => x.Players).FirstOrDefaultAsync(x => x.Id == id);
        if (game is null)
        {
            return NotFound();
        }
        GameContext.PlayedGames.Remove(game);
        await GameContext.SaveChangesAsync();
        return NoContent();
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<PlayedGame>> ChangePlayedGame(Guid id, PlayedGame game)
    {
        var existingGame = await GameContext.PlayedGames
                                .Include(x => x.Players)
                                .Where(x => x.Id == id)    
                                .FirstOrDefaultAsync();
                                
        if (existingGame is null) 
        { 
            return NotFound(); 
        }

        existingGame.GameName = game.GameName;
        existingGame.StartedAtUtc = game.StartedAtUtc;
        existingGame.DurationMinutes = game.DurationMinutes;
        foreach (var player in game.Players) 
        {
           var existingPlayer = existingGame.Players.FirstOrDefault(existingPlayer => existingPlayer.Id == player.Id);
            if (existingPlayer is null) 
            { 
                 existingGame.Players.Add(player);
            }
            else 
            { 
              existingPlayer.Name = player.Name;
              existingPlayer.IsWinner = player.IsWinner;
            }
        }
        var playerIds = game.Players.Select(player => player.Id).ToList();
        existingGame.Players.RemoveAll(existingPlayer => !playerIds.Contains(existingPlayer.Id) );

        await GameContext.SaveChangesAsync();
        return game;
    }
    

    


}