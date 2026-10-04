using DMello.Application.Customers.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace DMello.Application.Customers.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerOrderRepository _repository;

        public CustomerService(ICustomerOrderRepository repository)
        {
            _repository = repository;
        }
        public Task<CustomerOrderResponseDto?> GetByIdAsync(int id) =>
        _repository.GetByIdAsync(id);
        public Task<CustomerOrderResponseDto> CreateOrderAsync(CreateCustomerOrderDto dto) =>
            _repository.CreateAsync(dto);

        public Task<IEnumerable<CustomerOrderResponseDto>> GetAllOrdersAsync() =>
            _repository.GetAllAsync();
    }
}
