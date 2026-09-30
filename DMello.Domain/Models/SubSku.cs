using System;
using System.Collections.Generic;
using System.Text;

namespace DMello.Domain.Models
{
    public class SubSku
    {
        // Why it exists: Stores physical item variants including location, supplier, and image details.
        // What problem it solves: Prevents cluttering the main SKU table with variant-specific inventory locations and supplier data.
        // How it works: Belongs to a ProductSku via ProductSkuId foreign key and holds specific warehouse metadata.
        public Guid Id { get; set; } = Guid.NewGuid();
        public string SubSkuCode { get; set; } = string.Empty; // e.g., "HD-COTTON-01-L"
        public string Qty { get; set; } = string.Empty;       // e.g., "Large"
        public string? ImageUrl { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        // Foreign Key to Master Product
        public Guid ProductSkuId { get; set; }
        public ProductSku? ProductSku { get; set; }

        // Foreign Keys to Normalized Masters
        public Guid SupplierId { get; set; }
        public Supplier? Supplier { get; set; }

        //public Guid LocationId { get; set; }
        public WarehouseLocation? Location { get; set; }

        //Now Supplier and Location are seperate tables, so that we can prevent data duplicate, other supplier name and location will duplicate
    }
}
