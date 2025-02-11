using Microsoft.EntityFrameworkCore;
using STIVE.API.Database;
using STIVE.API.DTOs;
using STIVE.API.Interfaces;
using STIVE.API.Models;

namespace STIVE_API.Services;

public class BonusService : IBonusService
{
    private readonly StiveDbContext _context;

    public BonusService(StiveDbContext context)
    {
        _context = context;
    }

    public async Task<List<BonusDto>> GetAllBonusAsync()
    {
        var bonus =  await _context.Bonus.ToListAsync();
        return bonus.Select(b => new BonusDto
        {
            Id = b.Id,
            Name = b.Name,
            Description = b.Description,
            Duration = b.Duration,
            Price = b.Price,
        }).ToList();
    }

    public async Task<BonusDto> GetBonusByIdAsync(int id)
    {
        var bonus = await _context.Bonus.FindAsync(id);
        if (bonus == null)
            return null;

        return new BonusDto
        {
            Id = bonus.Id,
            Name = bonus.Name,
            Description = bonus.Description,
            Duration = bonus.Duration,
            Price = bonus.Price,
        };
    }

    public async Task<Bonus> CreateBonusAsync(CreateBonusDto createBonusDto)
    {
        var bonus = new Bonus
        {
            Name = createBonusDto.Name,
            Description = createBonusDto.Description,
            Duration = createBonusDto.Duration,
            Price = createBonusDto.Price
        };

        _context.Bonus.Add(bonus);
        await _context.SaveChangesAsync();
        
        return bonus;
    }

    public async Task<Bonus> UpdateBonusAsync(int id, BonusDto dto)
    {
        var bonus = await _context.Bonus.FindAsync(id);
        if (bonus == null)
            return null;

        bonus.Name = dto.Name;
        bonus.Description = dto.Description;
        bonus.Duration = dto.Duration;
        bonus.Price = dto.Price;

        await _context.SaveChangesAsync();
        return bonus;
    }

    public async Task DeleteBonusAsync(int id)
    {
        var bonus = await _context.Bonus.FindAsync(id);
        if (bonus == null)
            throw new KeyNotFoundException($"Bonus with ID {id} not found.");

        _context.Bonus.Remove(bonus);
        await _context.SaveChangesAsync();
    }
}