using DMello.Domain.Interfaces;
using DMello.Domain.Models;
using DMello.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace DMello.Infrastructure.Repositories
{
    // Why it exists: Database access class implementing EF Core query logic for Product Page entities.
    // What problem it solves: Keeps database code out of business services to follow Clean Architecture.
    // How it works: Queries DB context for ProductSkus and eager-loads child SubSkus.
    public class InventoryProductRepository : IInventoryRepository
    {
        private readonly ApplicationDbContext _context;

        public InventoryProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<ProductSku>> GetProductSkusWithDetailsAsync(string? searchQuery = null)
        {
            var query = _context.Set<ProductSku>()
         .Include(p => p.SubSkus)
             .ThenInclude(s => s.Supplier)        // <-- Eager load Supplier entity
         .Include(p => p.SubSkus)
             .ThenInclude(s => s.Location)    // <-- Eager load WarehouseLocation entity
         .AsNoTracking()
         .AsQueryable();

            return await query.ToListAsync();
        }

        public async Task<ProductSku> CreateProductAsync(ProductSku productSku) // Method data will be getted from Supplier
        {
            await _context.ProductSkus.AddAsync(productSku);
            await _context.SaveChangesAsync();
            return productSku;
        }
    }
}
