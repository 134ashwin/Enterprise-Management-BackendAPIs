using DMello.Application.Customers.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace DMello.Application.Customers
{
    public interface ICustomerOrderRepository
    {
        Task<CustomerOrderResponseDto> CreateAsync(CreateCustomerOrderDto dto);
        Task<IEnumerable<CustomerOrderResponseDto>> GetAllAsync();
        Task<CustomerOrderResponseDto?> GetByIdAsync(int id);
    }
}
