using Microsoft.AspNetCore.Mvc;
using STIVE.API.DTOs;
using STIVE.API.Models;

namespace STIVE.API.Interfaces;

public interface ICustomerService
{
    Task<List<CustomerDto>> GetAllCustomersAsync();
    Task<CustomerDto> GetCustomerByIdAsync(int customerId);
    Task<Customer> CreateCustomerAsync(CreateCustomerDto createCustomerDto);
    Task<Customer> UpdateCustomerAsync(int customerId, CustomerDto customerDto);
    Task DeleteCustomerAsync(int customerId);
}