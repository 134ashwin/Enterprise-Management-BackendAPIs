using System;
using System.Collections.Generic;
using System.Text;

namespace DMello.Application.InventoryProducts.DTOs
{
    public class ProductResponseDto
    {
        public Guid Id { get; set; }
        public string Sku { get; set; } = string.Empty;
        public string MainSku { get; set; } = string.Empty;
        public bool IsExpanded { get; set; } = false;
        public List<SubSkuResponseDto> SubSkus { get; set; } = new();
    }
}
