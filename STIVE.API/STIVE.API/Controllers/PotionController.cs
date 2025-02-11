using Microsoft.AspNetCore.Mvc;
using STIVE.API.DTOs;
using STIVE.API.Interfaces;

namespace STIVE_API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PotionController : ControllerBase
{
    private readonly IPotionService _potionService;

    public PotionController(IPotionService potionService)
    {
        _potionService = potionService;
    }

    [HttpGet("getAll")]
    public async Task<IActionResult> GetAllPotions()
    {
        var potions = await _potionService.GetAllPotionsAsync();
        return Ok(potions);
    }
    
    [HttpGet("getById/{potionId}")]
    public async Task<IActionResult> GetPotionById(int potionId)
    {
        var potion = await _potionService.GetPotionByIdAsync(potionId);
        if (potion == null)
            return NotFound();

        return Ok(potion);
    }
    
    [HttpPost("create")]
    public async Task<IActionResult> CreatePotion([FromBody] CreatePotionDto createPotionDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var potion = await _potionService.CreatePotionAsync(createPotionDto);

        return CreatedAtAction(nameof(GetPotionById), new { potionId = potion.Id }, potion);
    }

    [HttpPut("update/{potionId}")]
    public async Task<IActionResult> UpdatePotion(int potionId, [FromBody] PotionDto potionDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var potion = await _potionService.UpdatePotionAsync(potionId, potionDto);
        if (potion == null)
            return NotFound();

        return Ok(potion);
    }

    [HttpDelete("delete/{potionId}")]
    public async Task<IActionResult> DeletePotion(int potionId)
    {
        await _potionService.DeletePotionAsync(potionId);
        return Ok();
    }
}