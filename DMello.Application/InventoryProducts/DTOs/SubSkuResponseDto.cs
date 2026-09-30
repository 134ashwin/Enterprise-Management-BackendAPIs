using System;
using System.Collections.Generic;
using System.Text;

namespace DMello.Application.InventoryProducts.DTOs
{
    public class SubSkuResponseDto
    {
        public Guid Id { get; set; }
        public string SubSku { get; set; } = string.Empty;
        public string Qty { get; set; } = string.Empty;
        public string? ImageUrl { get; set; }
        public string Location { get; set; } = string.Empty;
        public string SupplierName { get; set; } = string.Empty;
        public string CreatedDate { get; set; } = string.Empty;
    }
}
