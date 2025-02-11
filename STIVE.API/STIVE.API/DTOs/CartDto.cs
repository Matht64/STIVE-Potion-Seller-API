using STIVE.API.Models;

namespace STIVE.API.DTOs;

public class CartDto
{
    public string UserId { get; set; }
    public int GameDataId { get; set; }
    public List<CartItemDto> CartItems { get; set; } = new List<CartItemDto>();
}