using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DMello.Application.Customers.DTOs;
using DMello.Domain.Interfaces;
using DMello.Domain.Models;

namespace DMello.Application.Customers.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _repository;

        public CustomerService(ICustomerRepository repository)
        {
            _repository = repository;
        }

        public async Task<CustomerResponseDto> CreateCustomerAsync(CustomerCreateDto dto)
        {
            // Business logic, mapping, and routing checks
            var trimmedGst = string.IsNullOrWhiteSpace(dto.GstNumber) ? null : dto.GstNumber.Trim();
            var trimmedEmail = string.IsNullOrWhiteSpace(dto.Email) ? null : dto.Email.Trim();

            var entity = new Customer
            {
                Name = dto.Name.Trim(),
                MobileNumber = dto.MobileNumber.Trim(),
                Email = trimmedEmail,
                GstNumber = trimmedGst,
                address = dto.address.Trim(),
                description = dto.description?.Trim()
            };

            var created = await _repository.CreateCustomerAsync(entity);
            return MapToResponseDto(created);
        }

        public async Task<IEnumerable<CustomerResponseDto>> GetAllCustomersAsync()
        {
            var customers = await _repository.GetAllCustomersAsync();
            return customers.Select(MapToResponseDto);
        }

        public async Task<IEnumerable<CustomerResponseDto>> GetB2BCustomersAsync()
        {
            var customers = await _repository.GetB2BCustomersAsync();
            return customers.Select(MapToResponseDto);
        }

        public async Task<IEnumerable<CustomerResponseDto>> GetB2CCustomersAsync()
        {
            var customers = await _repository.GetB2CCustomersAsync();
            return customers.Select(MapToResponseDto);
        }

        public async Task<CustomerResponseDto?> GetByIdAsync(int id)
        {
            var customer = await _repository.GetByIdAsync(id);
            return customer == null ? null : MapToResponseDto(customer);
        }

        private static CustomerResponseDto MapToResponseDto(Customer entity)
        {
            return new CustomerResponseDto
            {
                Id = entity.Id,
                Name = entity.Name,
                MobileNumber = entity.MobileNumber,
                Email = entity.Email,
                // Transform null or empty GST numbers to "No GST Number" for response output
                GstNumber = string.IsNullOrWhiteSpace(entity.GstNumber) ? "No GST Number" : entity.GstNumber,
                address = entity.address,
                description = entity.description,
                IsB2B = entity.IsB2B
            };
        }
    }
}
