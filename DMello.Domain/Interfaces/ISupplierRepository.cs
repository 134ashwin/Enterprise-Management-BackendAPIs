using DMello.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DMello.Domain.Interfaces
{
    public interface ISupplierRepository
    {
        Task<List<Supplier>> GetAllAsync();
        Task<Supplier?> GetByIdAsync(Guid id);
        Task<Supplier> CreateAsync(Supplier supplier);
        Task<bool> ExistsAsync(Guid supplierId);
    }
}
