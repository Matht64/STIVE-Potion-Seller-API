using Microsoft.AspNetCore.Mvc;
using STIVE.API.DTOs;
using STIVE.API.Interfaces;

namespace STIVE_API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomerController : ControllerBase
{
    private readonly ICustomerService _customerService;

    public CustomerController(ICustomerService customerService)
    {
        _customerService = customerService;
    }

    [HttpGet("getAll")]
    public async Task<IActionResult> GetAllCustomers()
    {
        var customers = await _customerService.GetAllCustomersAsync();
        return Ok(customers);
    }

    [HttpGet("getById/{customerId}")]
    public async Task<IActionResult> GetCustomerById(int customerId)
    {
        var customer = await _customerService.GetCustomerByIdAsync(customerId);
        if (customer == null)
            return NotFound();

        return Ok(customer);
    }

    [HttpPost("create")]
    public async Task<IActionResult> Create([FromBody] CreateCustomerDto createCustomerDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        var customer = await _customerService.CreateCustomerAsync(createCustomerDto);
        return CreatedAtAction(nameof(GetCustomerById), new { customerId = customer.Id }, customer);
    }

    [HttpPut("update/{customerId}")]
    public async Task<IActionResult> UpdateCustomer(int customerId, [FromBody] CustomerDto customerDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        var updatedCustomer = await _customerService.UpdateCustomerAsync(customerId, customerDto);
        return Ok(updatedCustomer);
    }

    [HttpDelete("delete/{customerId}")]
    public async Task<IActionResult> DeleteCustomer(int customerId)
    {
        await _customerService.DeleteCustomerAsync(customerId);
        return Ok();
    }
}