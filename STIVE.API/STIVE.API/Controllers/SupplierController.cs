using Microsoft.AspNetCore.Mvc;
using STIVE.API.DTOs;
using STIVE.API.Interfaces;

namespace STIVE_API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SupplierController : ControllerBase
{
    private readonly ISupplierService _supplierService;

    public SupplierController(ISupplierService supplierService)
    {
        _supplierService = supplierService;
    }

    [HttpGet("getAll")]
    public async Task<IActionResult> GetAllSuppliers()
    {
        var suppliers = await _supplierService.GetAllSuppliersAsync();
        return Ok(suppliers);
    }
    
    [HttpGet("getById/{supplierId}")]
    public async Task<IActionResult> GetSupplierById(int supplierId)
    {
        var supplier = await _supplierService.GetSupplierByIdAsync(supplierId);
        if (supplier == null)
            return NotFound();

        return Ok(supplier);
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateSupplierAsync(CreateSupplierDto createSupplierDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        var createdSupplier = await _supplierService.CreateSupplierAsync(createSupplierDto);
        return CreatedAtAction(nameof(GetSupplierById), new { SupplierId = createdSupplier.Id }, createdSupplier);
    }

    [HttpPut("update/{supplierId}")]
    public async Task<IActionResult> UpdateSupplier(int supplierId, [FromBody] SupplierDto supplierDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        var supplier = await _supplierService.UpdateSupplierAsync(supplierId, supplierDto);
        return Ok(supplier);
    }

    [HttpDelete("delete/{supplierId}")]
    public async Task<IActionResult> DeleteSupplier(int supplierId)
    {
        await _supplierService.DeleteSupplierAsync(supplierId);
        return Ok();
    }
}