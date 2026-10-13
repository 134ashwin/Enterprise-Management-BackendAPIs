using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DMello.Domain.Interfaces;
using DMello.Domain.Models;
using DMello.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DMello.Infrastructure.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ApplicationDbContext _context;

        public CustomerRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Customer> CreateCustomerAsync(Customer customer)
        {
            await _context.Customers.AddAsync(customer);
            await _context.SaveChangesAsync();
            return customer;
        }

        public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
        {
            return await _context.Customers
                .AsNoTracking()
                .OrderByDescending(c => c.Id)
                .ToListAsync();
        }

        public async Task<IEnumerable<Customer>> GetB2BCustomersAsync()
        {
            return await _context.Customers
                .AsNoTracking()
                .Where(c => c.GstNumber != null && c.GstNumber != "")
                .OrderByDescending(c => c.Id)
                .ToListAsync();
        }

        public async Task<IEnumerable<Customer>> GetB2CCustomersAsync()
        {
            return await _context.Customers
                .AsNoTracking()
                .Where(c => c.GstNumber == null || c.GstNumber == "")
                .OrderByDescending(c => c.Id)
                .ToListAsync();
        }

        public async Task<Customer?> GetByIdAsync(int id)
        {
            return await _context.Customers
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}
