using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using STIVE_API.Services;
using STIVE.API.Interfaces;
using STIVE.API.Services;

namespace STIVE_API.Controllers;

[Route("api/[controller]")]
[ApiController]

public class GameDataBonusController : Controller
{
    private readonly GameDataBonusService _gameDataBonusService;

    public GameDataBonusController(GameDataBonusService gameDataBonusService)
    {
        _gameDataBonusService = gameDataBonusService;
    }

    [HttpGet("getByGameDataId/{gameDataId}")]
    public async Task<IActionResult> GetBonusesForGameData(int gameDataId)
    {
        var potions = await _gameDataBonusService.GetBonusesForGameDataAsync(gameDataId); 
        return Ok(potions);
    }
    
    [HttpGet("getByBonusId/{bonusId}")]
    public async Task<IActionResult> GetGameDatasForBonus(int bonusId)
    {
        var gameData = await _gameDataBonusService.GetGameDatasForBonus(bonusId); 
        return Ok(gameData);
    }
    
    [HttpPost("add/{gameDataId}/{bonusId}")]
    public async Task<IActionResult> AddBonusToGameData(int gameDataId, int bonusId, int quantity)
    {
        try
        {
            await _gameDataBonusService.AddBonusToGameDataAsync(gameDataId, bonusId, quantity);
            return Ok(new { message = "Bonus ajouté avec succès." });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
    
    [HttpPost("use")]
    public async Task<IActionResult> UseBonus(int gameDataId, int bonusId)
    {
        try
        {
            await _gameDataBonusService.UseBonusAsync(gameDataId, bonusId);
            return Ok("Bonus utilisé avec succès.");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    
    [HttpDelete("delete/{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteGameDataBonus(int id)
    {
        var result = await _gameDataBonusService.DeleteGameDataBonusAsync(id);

        if (!result)
            return NotFound(new { message = "GameDataBonus not found." });

        return Ok(new { message = "GameDataBonus successfully deleted." });
    }
}