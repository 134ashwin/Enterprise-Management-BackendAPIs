using DMello.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DMello.Domain.Interfaces
{

//    Why it exists: IInventoryRepository is defined in the DMello.Domain.Interfaces namespace, which hasn't been imported into ProductService.cs or is missing the method contract.
//What problem it solves: Fixes the C# compilation error by bringing the repository interface into scope.
    public interface IInventoryRepository
    {
        Task<List<ProductSku>> GetProductSkusWithDetailsAsync(string? searchQuery = null);
    }
}
