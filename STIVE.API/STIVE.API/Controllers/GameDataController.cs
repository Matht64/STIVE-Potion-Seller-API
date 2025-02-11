using Microsoft.AspNetCore.Mvc;
using STIVE_API.Services;
using STIVE.API.DTOs;
using STIVE.API.Interfaces;
using STIVE.API.Models;

namespace STIVE_API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GameDataController : ControllerBase
{
    private readonly IGameDataService _gameDataService;

    public GameDataController(IGameDataService gameDataService)
    {
        _gameDataService = gameDataService;
    }

    [HttpGet("getAll")]
    public async Task<IActionResult> GetAllGameData()
    {
        var gameDatas = await _gameDataService.GetAllGameDataAsync();
        return Ok(gameDatas);
    }

    [HttpGet("getById/{gamedataId}")]
    public async Task<ActionResult<GameData>> GetById(int gamedataId)
    {
        var gameData = await _gameDataService.GetByIdAsync(gamedataId);
        if (gameData == null)
            return NotFound();
        
        return Ok(gameData);
    }
    
    [HttpGet("getAllByUserId/{userId}")]
    public async Task<ActionResult<List<GameData>>> GetAllByUserId(string userId)
    {
        var gameDataList = await _gameDataService.GetAllByUserIdAsync(userId);

        if (gameDataList == null || !gameDataList.Any())
            return NotFound($"No GameData found for user with ID: {userId}");

        return Ok(gameDataList);
    }
    
    [HttpPost("create")]
    public async Task<IActionResult> CreateAsync([FromBody] CreateGameDataDto createGameDataDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        var gamedata = await _gameDataService.CreateGameDataAsync(createGameDataDto);

        return CreatedAtAction(nameof(GetById), new { gamedataId = gamedata.Id }, gamedata);
    }

    [HttpPut("update/{gamedataId}")]
    public async Task<IActionResult> Update(int gamedataId, [FromBody] UpdateGameDataDto updateGameDataDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        var gameData = await _gameDataService.UpdateGameDataAsync(gamedataId, updateGameDataDto);
        return Ok(gameData);
    }

    [HttpDelete("delete/{gamedataId}")]
    public async Task<ActionResult> Delete(int gamedataId)
    {
        await _gameDataService.DeleteGameDataAsync(gamedataId);
        return Ok();
    }
}