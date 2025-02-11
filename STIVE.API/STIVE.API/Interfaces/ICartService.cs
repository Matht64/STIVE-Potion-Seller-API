using STIVE.API.DTOs;

namespace STIVE.API.Interfaces;

public interface ICartService
{
    Task<List<CartDto>> GetCartByUserAsync(string userId);
    Task<CartDto> AddToCartAsync(string userId, int gameDataId, int bonusId, int quantity);
    Task<bool> RemoveFromCartAsync(string userId, int gameDataId, int bonusId);
    Task<bool> ClearCartAsync(string userId, int gameDataId);
    Task<bool> ValidateCartAsync(string userId, int gameDataId);
}