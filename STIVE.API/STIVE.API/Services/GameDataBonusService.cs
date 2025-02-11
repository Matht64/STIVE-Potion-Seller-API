using Microsoft.EntityFrameworkCore;
using STIVE.API.Database;
using STIVE.API.DTOs;
using STIVE.API.Interfaces;
using STIVE.API.Models;

namespace STIVE.API.Services;

public class GameDataBonusService
{
    private readonly StiveDbContext _context;

    public GameDataBonusService(StiveDbContext context)
    {
        _context = context;
    }

    public async Task<List<GameDataBonus>> GetBonusesForGameDataAsync(int gameDataId)
    {
        return await _context.GameDataBonus
            .Where(gp => gp.GameDataId == gameDataId)
            .Include(gp => gp.Bonus)
            .ToListAsync();
    }
    public async Task<List<GameDataBonus>> GetGameDatasForBonus(int bonusId)
    {
        return await _context.GameDataBonus
            .Where(gp => gp.BonusId == bonusId)
            .Include(gp => gp.GameData)
            .ToListAsync();
    }
    public async Task AddBonusToGameDataAsync(int gameDataId, int bonusId, int quantity)
    {
        var existingEntry = await _context.GameDataBonus
            .FirstOrDefaultAsync(gb => gb.GameDataId == gameDataId && gb.BonusId == bonusId);

        if (existingEntry != null)
        {
            existingEntry.Quantity += quantity; 
            await _context.SaveChangesAsync();
            return;
        }

        var newEntry = new GameDataBonus
        {
            GameDataId = gameDataId,
            BonusId = bonusId,
            Quantity = quantity
        };

        _context.GameDataBonus.Add(newEntry);
        await _context.SaveChangesAsync();
    }
    
    public async Task UseBonusAsync(int gameDataId, int bonusId)
    {
        var existingEntry = await _context.GameDataBonus
            .FirstOrDefaultAsync(gb => gb.GameDataId == gameDataId && gb.BonusId == bonusId);

        if (existingEntry != null)
        {
            var bonus = await _context.Bonus
                .FirstOrDefaultAsync(b => b.Id == bonusId);

            if (bonus != null)
            {
                if (existingEntry.DateEnd > DateTime.Now)
                    existingEntry.DateEnd = null;
                else
                    existingEntry.DateEnd = DateTime.Now.AddHours(bonus.Duration);

                existingEntry.Quantity--;

                if (existingEntry.Quantity <= 0 && existingEntry.DateEnd > DateTime.Now)
                    _context.GameDataBonus.Remove(existingEntry);

                await _context.SaveChangesAsync();
            }
            else
                throw new Exception("Le bonus spécifié n'existe pas.");
        }
        else
            throw new Exception("Le GameDataBonus spécifié n'existe pas.");
    }
    
    public async Task<bool> DeleteGameDataBonusAsync(int gameDataBonusId)
    {
        var gameDataBonus = await _context.GameDataBonus.FindAsync(gameDataBonusId);

        if (gameDataBonus == null)
            return false;

        _context.GameDataBonus.Remove(gameDataBonus);
        await _context.SaveChangesAsync();
        return true;
    }
}