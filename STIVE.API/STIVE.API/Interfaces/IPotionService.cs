using STIVE.API.DTOs;
using STIVE.API.Models;

namespace STIVE.API.Interfaces;

public interface IPotionService
{
    Task<List<PotionDto>> GetAllPotionsAsync();
    Task<PotionDto> GetPotionByIdAsync(int potionId);
    Task<Potion> CreatePotionAsync(CreatePotionDto createPotionDto);
    Task<Potion> UpdatePotionAsync(int potionId, PotionDto potionDto);
    Task DeletePotionAsync(int potionId);
}