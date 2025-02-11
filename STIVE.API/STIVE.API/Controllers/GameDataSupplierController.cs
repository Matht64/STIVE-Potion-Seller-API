using Microsoft.AspNetCore.Mvc;
using STIVE.API.DTOs;
using STIVE.API.Services;

namespace STIVE_API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GameDataSupplierController : ControllerBase
{
    private readonly GameDataSupplierService _gameDataSupplierService;

    public GameDataSupplierController(GameDataSupplierService gameDataSupplierService)
    {
        _gameDataSupplierService = gameDataSupplierService;
    }
    
    [HttpGet("getByGameDataId/{gameDataId}")]
    public async Task<IActionResult> GetByGameDataId(int gameDataId)
    {
        var gameDataSuppliers = await _gameDataSupplierService.GetByGameDataIdAsync(gameDataId);
        return Ok(gameDataSuppliers);
    }

    [HttpGet("getBySupplierId/{supplierId}")]
    public async Task<IActionResult> GetBySupplierId(int supplierId)
    {
        var gameDataSuppliers = await _gameDataSupplierService.GetBySupplierIdAsync(supplierId);
        return Ok(gameDataSuppliers);
    }

    [HttpPost("add")]
    public async Task<IActionResult> AddGameDataSupplier([FromQuery] int gameDataId, [FromQuery] int supplierId)
    {
        try
        {
            var newGameDataSupplier = await _gameDataSupplierService.AddGameDataSupplierAsync(gameDataId, supplierId);
            return CreatedAtAction(nameof(GetByGameDataId), new { gameDataId = newGameDataSupplier.GameDataId }, newGameDataSupplier);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(ex.Message);
        }
    }

    [HttpDelete("delete")]
    public async Task<IActionResult> DeleteGameDataSupplier(int gameDataId, int supplierId)
    {
        var result = await _gameDataSupplierService.DeleteGameDataSupplierAsync(gameDataId, supplierId);
        if (!result)
            return NotFound($"GameDataSupplier with GameDataId {gameDataId} and SupplierId {supplierId} not found.");
        
        return NoContent();
    }
}