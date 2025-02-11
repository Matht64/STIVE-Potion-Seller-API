using STIVE.API.DTOs;
using STIVE.API.Models;

namespace STIVE.API.Interfaces;

public interface ISupplierService
{
    Task<List<SupplierDto>> GetAllSuppliersAsync();
    Task<SupplierDto> GetSupplierByIdAsync(int supplierId);
    Task<Supplier> CreateSupplierAsync(CreateSupplierDto createSupplierDto);
    Task<Supplier> UpdateSupplierAsync(int supplierId, SupplierDto supplierDto);
    Task DeleteSupplierAsync(int supplierId);
}