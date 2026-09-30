using DMello.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

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
        public string Qty { get; set; } = string.Empty;
        public string? ImageUrl { get; set; }

        // Foreign Keys linking to normalized master tables
        public string SupplierName { get; set; } = string.Empty;
        public string GSTNumber { get; set; } = string.Empty;
        
        public string? LocationCode { get; set; }
    }
}
