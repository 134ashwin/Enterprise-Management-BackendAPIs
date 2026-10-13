using System.Collections.Generic;
using System.Threading.Tasks;
using DMello.Application.Customers.DTOs;

namespace DMello.Application.Customers
{
    public interface ICustomerService
    {
        Task<CustomerResponseDto> CreateCustomerAsync(CustomerCreateDto dto);
        Task<IEnumerable<CustomerResponseDto>> GetAllCustomersAsync();
        Task<IEnumerable<CustomerResponseDto>> GetB2BCustomersAsync();
        Task<IEnumerable<CustomerResponseDto>> GetB2CCustomersAsync();
        Task<CustomerResponseDto?> GetByIdAsync(int id);
    }
}
