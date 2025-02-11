using Microsoft.EntityFrameworkCore;
using STIVE.API.Database;
using STIVE.API.DTOs;
using STIVE.API.Interfaces;
using STIVE.API.Models;

namespace STIVE.API.Services;

public class PotionService : IPotionService
{
    private readonly StiveDbContext _context;

    public PotionService(StiveDbContext context)
    {
        _context = context;
    }

    public async Task<List<PotionDto>> GetAllPotionsAsync()
    {
        var potions = await _context.Potion.ToListAsync();
        return potions.Select(p => new PotionDto
        {
            Id = p.Id,
            Name = p.Name,
            Price = p.Price,
            Picture = p.Picture
        }).ToList();
    }

    public async Task<PotionDto> GetPotionByIdAsync(int id)
    {
        var potion = await _context.Potion.FindAsync(id);
        if (potion == null)
            return null;

        return new PotionDto
        {
            Id = potion.Id,
            Name = potion.Name,
            Price = potion.Price,
            Picture = potion.Picture
        };
    }
    
    public async Task<Potion> CreatePotionAsync(CreatePotionDto createPotionDto)
    {
        var potion = new Potion
        {
            Name = createPotionDto.Name,
            Price = createPotionDto.Price,
            Picture = createPotionDto.Picture
        };

        _context.Potion.Add(potion);
        await _context.SaveChangesAsync();

        return potion;
    }
    
    public async Task<Potion> UpdatePotionAsync(int id, PotionDto potionDto)
    {
        var potion = await _context.Potion.FindAsync(id);
        if (potion == null)
            return null;

        potion.Name = potionDto.Name;
        potion.Price = potionDto.Price;
        potion.Picture = potionDto.Picture;

        await _context.SaveChangesAsync();
        return potion;
    }

    public async Task DeletePotionAsync(int id)
    {
        var potion = await _context.Potion.FindAsync(id);
        if (potion == null)
            throw new KeyNotFoundException($"Potion with ID {id} not found.");
        
        _context.Potion.Remove(potion);
        await _context.SaveChangesAsync();
    }
}