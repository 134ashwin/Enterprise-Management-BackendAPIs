using DMello.Application.Customers.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace DMello.Application.Customers
{
    public interface ICustomerService
    {
        Task<CustomerOrderResponseDto> CreateOrderAsync(CreateCustomerOrderDto dto);
        Task<IEnumerable<CustomerOrderResponseDto>> GetAllOrdersAsync();
        Task<CustomerOrderResponseDto?> GetByIdAsync(int id);
    }
}
