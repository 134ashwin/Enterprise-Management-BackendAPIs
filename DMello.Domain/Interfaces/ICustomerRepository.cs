using System.Collections.Generic;
using System.Threading.Tasks;
using DMello.Domain.Models;

namespace DMello.Domain.Interfaces
{
    public interface ICustomerRepository
    {
        Task<Customer> CreateCustomerAsync(Customer customer);
        Task<IEnumerable<Customer>> GetAllCustomersAsync();
        Task<IEnumerable<Customer>> GetB2BCustomersAsync();
        Task<IEnumerable<Customer>> GetB2CCustomersAsync();
        Task<Customer?> GetByIdAsync(int id);
    }
}
