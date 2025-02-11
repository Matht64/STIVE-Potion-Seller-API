using Microsoft.EntityFrameworkCore;
using STIVE.API.Database;
using STIVE.API.DTOs;
using STIVE.API.Interfaces;
using STIVE.API.Models;

namespace STIVE.API.Services;

public class SupplierService : ISupplierService
{
    private readonly StiveDbContext _context;

    public SupplierService(StiveDbContext context)
    {
        _context = context;
    }

    public async Task<List<SupplierDto>> GetAllSuppliersAsync()
    {
        var supplier = await _context.Supplier.ToListAsync();
        return supplier.Select(s => new SupplierDto
        {
            Id = s.Id,
            Name = s.Name,
            Picture = s.Picture,
            PotionId = s.PotionId
        }).ToList();
    }

    public async Task<SupplierDto> GetSupplierByIdAsync(int id)
    {
        var supplier = await _context.Supplier.FindAsync(id);
        if (supplier == null)
            return null;

        return new SupplierDto
        {
            Id = supplier.Id,
            Name = supplier.Name,
            Picture = supplier.Picture,
            PotionId = supplier.PotionId
        };
    }

    public async Task<Supplier> CreateSupplierAsync(CreateSupplierDto createSupplierDto)
    {
        var supplier = new Supplier
        {
            Name = createSupplierDto.Name,
            Picture = createSupplierDto.Picture,
            PotionId = createSupplierDto.PotionId
        };

        _context.Supplier.Add(supplier);
        await _context.SaveChangesAsync();

        return supplier;
    }

    public async Task<Supplier> UpdateSupplierAsync(int id, SupplierDto dto)
    {
        var supplier = await _context.Supplier.FindAsync(id);
        if (supplier == null)
            return null;

        var potion = await _context.Potion.FindAsync(dto.PotionId);
        if (potion == null)
            return null;
        
        supplier.Name = dto.Name;
        supplier.Picture = dto.Picture;
        supplier.PotionId = dto.PotionId;

        await _context.SaveChangesAsync();
        return supplier;
    }

    public async Task DeleteSupplierAsync(int id)
    {
        var supplier = await _context.Supplier.FindAsync(id);
        if (supplier == null)
            throw new Exception("Supplier not found");

        _context.Supplier.Remove(supplier);
        await _context.SaveChangesAsync();
    }
}