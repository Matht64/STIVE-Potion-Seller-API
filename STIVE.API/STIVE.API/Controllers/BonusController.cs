using Microsoft.AspNetCore.Mvc;
using STIVE.API.DTOs;
using STIVE.API.Interfaces;

namespace STIVE_API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BonusController : ControllerBase
{
    private readonly IBonusService _bonusService;

    public BonusController(IBonusService bonusService)
    {
        _bonusService = bonusService;
    }

    [HttpGet("getAll")]
    public async Task<IActionResult> GetAllBonus()
    {
        var bonuses = await _bonusService.GetAllBonusAsync();
        return Ok(bonuses);
    }

    [HttpGet("getById/{bonusId}")]
    public async Task<IActionResult> GetBonusById(int bonusId)
    {
        var bonus = await _bonusService.GetBonusByIdAsync(bonusId);
        if (bonus == null)
            return NotFound();
            
        return Ok(bonus);
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateBonus([FromBody] CreateBonusDto createBonusDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        var createdBonus = await _bonusService.CreateBonusAsync(createBonusDto);
        return CreatedAtAction(nameof(GetBonusById), new { bonusId = createdBonus.Id }, createdBonus);
    }

    [HttpPut("update/{bonusId}")]
    public async Task<IActionResult> UpdateBonus(int bonusId, [FromBody] BonusDto bonusDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        var bonus = await _bonusService.UpdateBonusAsync(bonusId, bonusDto);
        return Ok(bonus);
    }

    [HttpDelete("delete/{bonusId}")]
    public async Task<ActionResult> DeleteBonus(int bonusId)
    {
        await _bonusService.DeleteBonusAsync(bonusId);
        return Ok();
    }
}
