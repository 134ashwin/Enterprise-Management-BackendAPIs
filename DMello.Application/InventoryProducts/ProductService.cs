using DMello.Application.InventoryProducts.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using DMello.Domain.Interfaces;

namespace DMello.Application.InventoryProducts
{
    // Why it exists: Executes business logic and transforms domain models into API response DTOs.
    // What problem it solves: Keeps controllers thin by removing query mapping logic from the API layer.
    // How it works: Fetches products from repository, filters by query, and projects entities into ProductResponseDto objects.
    public class ProductService : IProductService
    {
        private readonly IInventoryRepository _inventoryRepository;

        public ProductService(IInventoryRepository inventoryRepository)
        {
            _inventoryRepository = inventoryRepository;
        }

        public async Task<List<ProductResponseDto>> GetProductsAsync(string? searchQuery = null)
        {
            var products = await _inventoryRepository.GetProductSkusWithDetailsAsync(searchQuery);

            return products.Select(p => new ProductResponseDto
            {
                Id = p.Id,
                Sku = p.Sku,
                MainSku = p.MainSku,
                IsExpanded = false,
                SubSkus = p.SubSkus.Select(s => new SubSkuResponseDto
                {
                    Id = s.Id,
                    SubSku = s.SubSkuCode,
                    Size = s.Size,
                    ImageUrl = s.ImageUrl,
                    // Access string property on Supplier object (with null check default)
                    SupplierName = s.Supplier?.Name ?? "N/A",
                    // Access string property on WarehouseLocation object (with null check default)
                    Location = s.Location?.LocationCode ?? "N/A",
                    CreatedDate = s.CreatedDate.ToString("yyyy-MM-dd")
                }).ToList()
            }).ToList();
        }
    }
}
