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
        var doesGameExist = await GameContext.PlayedGames
                                .Where(x => x.Id == id)    
                                .AnyAsync();
                                
        if (!doesGameExist) 
        { 
            return NotFound(); 
        }
        game.Id = id;
        GameContext.PlayedGames.Update(game);
        await GameContext.SaveChangesAsync();
        return game;
    }
    

    


}