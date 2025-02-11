using Microsoft.AspNetCore.Mvc;
using STIVE.API.Models;
using STIVE.API.Services;

namespace STIVE_API.Controllers;

[ApiController]
[Route("api/[controller]")]

public class GameDataPotionController : ControllerBase
{
    private readonly GameDataPotionService _gameDataPotionService;

    public GameDataPotionController(GameDataPotionService gameDataPotionService)
    {
        _gameDataPotionService = gameDataPotionService;
    }
    
    [HttpGet("getByGameDataId/{gameDataId}")]
    public async Task<IActionResult> GetPotionsForGameData(int gameDataId)
    {
        var potions = await _gameDataPotionService.GetPotionsForGameDataAsync(gameDataId); 
        return Ok(potions);
    }
    
    [HttpGet("getByPotionId/{potionId}")]
    public async Task<IActionResult> GetGameDatasForPotion(int potionId)
    {
        var gameData = await _gameDataPotionService.GetGameDatasForPotion(potionId); 
        return Ok(gameData);
    }
        
    [HttpPost("add")]
    public async Task<IActionResult> AddPotionToGameData(int gameDataId, int potionId, int quantity)
    {
        await _gameDataPotionService.AddPotionToGameDataAsync(gameDataId, potionId, quantity);
        return Ok(new { message = "Potion added successfully." });
    }

    [HttpPost("remove")]
    public async Task<IActionResult> RemovePotionFromGameData(int gameDataId, int potionId, int quantity)
    {
        await _gameDataPotionService.RemovePotionFromGameDataAsync(gameDataId, potionId, quantity);
        return Ok(new { message = "Potion removed successfully." });
    }

    [HttpDelete("delete")]
    public async Task<IActionResult> DeleteGameDataPotion(int gameDataId, int potionId)
    {
        try
        {
            await _gameDataPotionService.DeleteGameDataPotionAsync(gameDataId, potionId);
            return Ok();
        }
        catch (Exception ex)
        {
            return NotFound(new { Message = ex.Message });
        }
    }
}