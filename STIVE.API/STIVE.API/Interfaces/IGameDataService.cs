using STIVE.API.DTOs;
using STIVE.API.Models;

namespace STIVE.API.Interfaces;

public interface IGameDataService
{
    Task<List<GameDataDto>> GetAllGameDataAsync();
    Task<GameData> GetByIdAsync(int gamedataId);
    Task<List<GameDataDto>> GetAllByUserIdAsync(string userId);
    Task<GameData> CreateGameDataAsync(CreateGameDataDto userId);
    Task<GameData> UpdateGameDataAsync(int gamedataId, UpdateGameDataDto updateGameDataDto);
    Task<bool> DeleteGameDataAsync(int gamedataId);
}