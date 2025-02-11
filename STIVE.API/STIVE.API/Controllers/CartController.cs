using Microsoft.AspNetCore.Mvc;
using STIVE.API.DTOs;
using STIVE.API.Interfaces;

namespace STIVE_API.Controllers;

[ApiController]
[Route("api/[controller]")]

public class CartController : ControllerBase
{
    private readonly ICartService _cartService;

    public CartController(ICartService cartService)
    {
        _cartService = cartService;
    }

    [HttpGet("getByUserId/{userId}")]
    public async Task<IActionResult> GetCartByUser(string userId)
    {
        var cart = await _cartService.GetCartByUserAsync(userId);
        if (cart == null || !cart.Any())
            return NotFound();
        
        return Ok(cart);
    }
    
    [HttpPost("add")]
    public async Task<IActionResult> AddToCart([FromBody] AddToCartDto addToCartDto)
    {
        if (addToCartDto == null)
            return BadRequest();

        try
        {
            var cart = await _cartService.AddToCartAsync(addToCartDto.UserId, addToCartDto.GameDataId,
                addToCartDto.BonusId, addToCartDto.Quantity);
            return Ok(cart);
        }
        catch(ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("remove")]
    public async Task<IActionResult> RemoveFromCart([FromBody] RemoveToCartDto removeToCartDto)
    {
        var result = await _cartService.RemoveFromCartAsync(removeToCartDto.UserId, removeToCartDto.BonusId, removeToCartDto.GameDataId);
        if (!result)
            return NotFound();

        return Ok();
    }


    [HttpDelete("clear")]
    public async Task<IActionResult> ClearCart([FromBody] ClearCartDto clearCartDto)
    {
        var result = await _cartService.ClearCartAsync(clearCartDto.UserId, clearCartDto.GameDataId);
        if (!result)
            return NotFound();

        return Ok();
    }

    [HttpPost("Validate")]
    public async Task<IActionResult> ValidateCart([FromBody] ValidateCartDto validateCartDto)
    {
        var result = await _cartService.ValidateCartAsync(validateCartDto.UserId, validateCartDto.GameDataId);
        if (!result)
            return BadRequest();
        
        return Ok(result);
    }
}