using System;
using System.Collections.Generic;
using System.Text;
using DMello.Domain.Models;
using DMello.Domain.Interfaces;
using DMello.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DMello.Infrastructure.Repositories
{
    public class SupplierRepository : ISupplierRepository
    {
        private readonly ApplicationDbContext _context;

        public SupplierRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Supplier>> GetAllAsync()
        {
            return await _context.Suppliers.AsNoTracking().ToListAsync();
        }

        public async Task<Supplier?> GetByIdAsync(Guid id)
        {
            return await _context.Suppliers.AsNoTracking().FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<Supplier> CreateAsync(Supplier supplier)
        {
            await _context.Suppliers.AddAsync(supplier);
            await _context.SaveChangesAsync();
            return supplier;
        }
        public async Task<bool> ExistsAsync(Guid supplierId)
        {
            return await _context.Suppliers.AnyAsync(s => s.Id == supplierId);
        }
    }
}
