using Microsoft.EntityFrameworkCore;
using STIVE.API.Database;
using STIVE.API.DTOs;
using STIVE.API.Interfaces;
using STIVE.API.Models;

namespace STIVE.API.Services;

public class CustomerService : ICustomerService
{
    private readonly StiveDbContext _context;

    public CustomerService(StiveDbContext context)
    {
        _context = context;
    }

    public async Task<List<CustomerDto>> GetAllCustomersAsync()
    {
        var customer = await _context.Customer.ToListAsync();
        return customer.Select(c => new CustomerDto
        {
            Id = c.Id,
            Name = c.Name,
            Picture = c.Picture
        }).ToList();
    }

    public async Task<CustomerDto> GetCustomerByIdAsync(int id)
    {
        var customer = await _context.Customer.FindAsync(id);
        if (customer == null)
            return null;

        return new CustomerDto
        {
            Id = customer.Id,
            Name = customer.Name,
            Picture = customer.Picture
        };
    }

    public async Task<Customer> CreateCustomerAsync(CreateCustomerDto createCustomerDto)
    {
        var customer = new Customer
        {
            Name = createCustomerDto.Name,
            Picture = createCustomerDto.Picture
        };

        _context.Customer.Add(customer);
        await _context.SaveChangesAsync();

        return customer;
    }

    public async Task<Customer> UpdateCustomerAsync(int id, CustomerDto customerDto)
    {
        var customer = await _context.Customer.FindAsync(id);
        if (customer == null)
            return null;

        customer.Name = customerDto.Name;
        customer.Picture = customerDto.Picture;

        await _context.SaveChangesAsync();
        return customer;
    }

    public async Task DeleteCustomerAsync(int id)
    {
        var customer = await _context.Customer.FindAsync(id);
        if (customer == null)
        {
            throw new ArgumentException($"Customer with ID {id} does not exist.");
        }

        _context.Customer.Remove(customer);
        await _context.SaveChangesAsync();
    }
}