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

            if (!string.IsNullOrWhiteSpace(searchQuery))
            {
                var search = searchQuery.Trim().ToLower();  //Clean up what user typed(remove extra spaces like " shirt "-> "shirt"

                query = query.Where(p =>                  //Add a filter rule: Keep product if Sku OR MainSku contains the search word
                    p.Sku.ToLower().Contains(search) ||
                    p.MainSku.ToLower().Contains(search));
            }

            return await query.ToListAsync();
        }
    }
}
