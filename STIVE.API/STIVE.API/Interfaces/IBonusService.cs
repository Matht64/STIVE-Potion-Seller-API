using STIVE.API.DTOs;
using STIVE.API.Models;

namespace STIVE.API.Interfaces;

public interface IBonusService
{
    Task<List<BonusDto>> GetAllBonusAsync();
    Task<BonusDto> GetBonusByIdAsync(int bonusId);
    Task<Bonus> CreateBonusAsync(CreateBonusDto createBonusDto);
    Task<Bonus> UpdateBonusAsync(int bonusId, BonusDto bonusDto);
    Task DeleteBonusAsync(int bonusId);
}