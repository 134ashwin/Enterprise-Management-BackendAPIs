using System;
using System.Collections.Generic;
using System.Text;

namespace DMello.Application.InventoryProducts.DTOs
{
    public class CreateProductRequestDto
    {
        public string Sku { get; set; } = string.Empty;
        public string MainSku { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;

        // SubSku / Variant fields
        public string SubSkuCode { get; set; } = string.Empty;
        public string Size { get; set; } = string.Empty;
        public string? ImageUrl { get; set; }

        // Foreign Keys linking to normalized master tables
        public string SupplierName { get; set; } = string.Empty;
        public string GSTNumber { get; set; } = string.Empty;

        public string LocationCode { get; set; } = string.Empty;
    }
}
