using System;
using System.Collections.Generic;
using System.Text;

namespace DMello.Domain.Models
{
    // Why it exists: Represents a parent product SKU and its relationship to sub-SKUs in the domain.
    // What problem it solves: Prevents duplicate product entries and maintains a clean parent-child relationship for inventory hierarchy.
    // How it works: Stores master SKU info and links to a collection of child SubSku entities.
    public class ProductSku
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string MainSku { get; set; } = string.Empty; // e.g., "HOODIE-MAIN"
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<SubSku> SubSkus { get; set; } = new List<SubSku>();
    }
}
