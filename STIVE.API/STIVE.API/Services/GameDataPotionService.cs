using Microsoft.EntityFrameworkCore;
using STIVE.API.Database;
using STIVE.API.Models;

namespace STIVE.API.Services;

public class GameDataPotionService
{
    private readonly StiveDbContext _context;

    public GameDataPotionService(StiveDbContext context)
    {
        _context = context;
    }
    public async Task<List<GameDataPotion>> GetPotionsForGameDataAsync(int gameDataId)
    {
        return await _context.GameDataPotion
            .Where(gp => gp.GameDataId == gameDataId)
            .Include(gp => gp.Potion)
            .ToListAsync();
    }
    public async Task<List<GameDataPotion>> GetGameDatasForPotion(int potionId)
    {
        return await _context.GameDataPotion
            .Where(gp => gp.PotionId == potionId)
            .Include(gp => gp.GameData)
            .ToListAsync();
    }
    public async Task AddPotionToGameDataAsync(int gameDataId, int potionId, int quantity)
    {
        var existingEntry = await _context.GameDataPotion
            .FirstOrDefaultAsync(gp => gp.GameDataId == gameDataId && gp.PotionId == potionId);

        if (existingEntry != null)
        {
            existingEntry.Quantity += quantity;
        }
        else
        {
            var newEntry = new GameDataPotion
            {
                GameDataId = gameDataId,
                PotionId = potionId,
                Quantity = quantity
            };
            _context.GameDataPotion.Add(newEntry);
        }

        await _context.SaveChangesAsync();
    }

    public async Task RemovePotionFromGameDataAsync(int gameDataId, int potionId, int quantity)
    {
        var existingEntry = await _context.GameDataPotion
            .FirstOrDefaultAsync(gp => gp.GameDataId == gameDataId && gp.PotionId == potionId);

        if (existingEntry == null)
            throw new Exception("Potion not found for this game data.");

        if (existingEntry.Quantity <= quantity)
            _context.GameDataPotion.Remove(existingEntry);
        else
            existingEntry.Quantity -= quantity;

        await _context.SaveChangesAsync();
    }
    
    public async Task DeleteGameDataPotionAsync(int gameDataId, int potionId)
    {
        var entryToDelete = await _context.GameDataPotion
            .FirstOrDefaultAsync(gp => gp.GameDataId == gameDataId && gp.PotionId == potionId);

        if (entryToDelete == null)
            throw new Exception("GameDataPotion not found.");

        _context.GameDataPotion.Remove(entryToDelete);
        await _context.SaveChangesAsync();
    }
}