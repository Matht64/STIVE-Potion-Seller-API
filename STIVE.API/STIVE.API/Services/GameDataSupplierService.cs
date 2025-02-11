using Microsoft.EntityFrameworkCore;
using STIVE.API.Database;
using STIVE.API.DTOs;
using STIVE.API.Models;

namespace STIVE.API.Services;

public class GameDataSupplierService
{
    private readonly StiveDbContext _context;

    public GameDataSupplierService(StiveDbContext context)
    {
        _context = context;
    }
    
    public async Task<List<GameDataSupplier>> GetByGameDataIdAsync(int gameDataId)
    {
        return await _context.GameDataSupplier
            .Where(gds => gds.GameDataId == gameDataId)
            .Include(gds => gds.GameData)
            .Include(gds => gds.Supplier)
            .ToListAsync();
    }

    public async Task<List<GameDataSupplier>> GetBySupplierIdAsync(int supplierId)
    {
        return await _context.GameDataSupplier
            .Where(gds => gds.SupplierId == supplierId)
            .Include(gds => gds.GameData)
            .Include(gds => gds.Supplier)
            .ToListAsync();
    }
    
    public async Task<GameDataSupplier> AddGameDataSupplierAsync(int gameDataId, int supplierId)
    {
        var gameData = await _context.GameData.FindAsync(gameDataId);
        if (gameData == null)
        {
            throw new ArgumentException($"GameData with ID {gameDataId} does not exist.");
        }

        var supplier = await _context.Supplier.FindAsync(supplierId);
        if (supplier == null)
        {
            throw new ArgumentException($"Supplier with ID {supplierId} does not exist.");
        }

        var existingRelation = await _context.GameDataSupplier
            .FirstOrDefaultAsync(gds => gds.GameDataId == gameDataId && gds.SupplierId == supplierId);
        if (existingRelation != null)
        {
            throw new InvalidOperationException($"The GameDataSupplier relationship already exists.");
        }

        var newGameDataSupplier = new GameDataSupplier
        {
            GameDataId = gameDataId,
            SupplierId = supplierId
        };

        _context.GameDataSupplier.Add(newGameDataSupplier);
        await _context.SaveChangesAsync();
        return newGameDataSupplier;
    }

    public async Task<bool> DeleteGameDataSupplierAsync(int gameDataId, int supplierId)
    {
        var gameDataSupplier = await _context.GameDataSupplier
            .FirstOrDefaultAsync(gds => gds.GameDataId == gameDataId && gds.SupplierId == supplierId);

        if (gameDataSupplier == null)
            return false;

        _context.GameDataSupplier.Remove(gameDataSupplier);
        await _context.SaveChangesAsync();
        return true;
    }
}