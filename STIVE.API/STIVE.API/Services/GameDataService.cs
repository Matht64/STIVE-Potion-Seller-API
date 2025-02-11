using Microsoft.EntityFrameworkCore;
using STIVE.API.Database;
using STIVE.API.DTOs;
using STIVE.API.Interfaces;
using STIVE.API.Models;

namespace STIVE_API.Services;

public class GameDataService : IGameDataService
{
    private readonly StiveDbContext _context;

    public GameDataService(StiveDbContext context)
    {
        _context = context;
    }

    public async Task<List<GameDataDto>> GetAllGameDataAsync()
    {
        return await _context.GameData
            .Include(g => g.User)
            .Select(g => new GameDataDto
            {
                Id = g.Id,
                Gold = g.Gold,
                UserId = g.UserId,
                UserName = g.User != null ? g.User.UserName : null,
                GameDataBonuses = g.GameDataBonuses,
                GameDataPotions = g.GameDataPotions,
                GameDataSuppliers = g.GameDataSuppliers
            })
            .ToListAsync();
    }
    public async Task<GameData> GetByIdAsync(int id)
    {
        return await _context.GameData
            .Include(g => g.GameDataBonuses)
            .Include(g => g.GameDataPotions)
            .Include(g => g.GameDataSuppliers)
            .FirstOrDefaultAsync(g => g.Id == id);
    }

    public async Task<List<GameDataDto>> GetAllByUserIdAsync(string userId)
    {
        return await _context.GameData
            .Where(g => g.UserId == userId)
            .Include(g => g.User)
            .Select(g => new GameDataDto
            {
                Id = g.Id,
                Gold = g.Gold,
                UserId = g.UserId,
                UserName = g.User != null ? g.User.UserName : null,
                GameDataBonuses = g.GameDataBonuses,
                GameDataPotions = g.GameDataPotions,
                GameDataSuppliers = g.GameDataSuppliers
            })
            .ToListAsync();
    }

    public async Task<GameData> CreateGameDataAsync(CreateGameDataDto createGameDataDto)
    {
        var gamedata = new GameData
        {
            UserId = createGameDataDto.UserId,
            Gold = 100,
            GameDataBonuses = new List<GameDataBonus>(),
            GameDataPotions = new List<GameDataPotion>(),
            GameDataSuppliers = new List<GameDataSupplier>()
        };
        
        _context.GameData.Add(gamedata);
        await _context.SaveChangesAsync();
        
        return gamedata;
    }

    public async Task<GameData> UpdateGameDataAsync(int id, UpdateGameDataDto dto)
    {
        var gamedata = await _context.GameData.FindAsync(id);
        if (gamedata == null)
            return null;
        
        var user  = await _context.Users.FindAsync(dto.UserId);
        if (user == null)
            return null;
        
        gamedata.Gold = dto.Gold;
        gamedata.UserId = dto.UserId;
        
        await _context.SaveChangesAsync();
        return gamedata;
    }


    public async Task<bool> DeleteGameDataAsync(int id)
    {
        var gameData = await _context.GameData.FindAsync(id);
        if (gameData == null)
            return false;

        _context.GameData.Remove(gameData);
        await _context.SaveChangesAsync();
        return true;
    }
}