using DMello.Application.InventoryProducts.DTOs;
using DMello.Domain.Interfaces;
using DMello.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DMello.Application.InventoryProducts
{
    // Why it exists: Executes business logic and transforms domain models into API response DTOs.
    // What problem it solves: Keeps controllers thin by removing query mapping logic from the API layer.
    // How it works: Fetches products from repository, filters by query, and projects entities into ProductResponseDto objects.
    public class ProductService : IProductService
    {
        private readonly IInventoryRepository _inventoryRepository;
        private readonly ISupplierRepository _supplierRepository;

        public ProductService(IInventoryRepository inventoryRepository, ISupplierRepository supplierRepository)
        {
            _inventoryRepository = inventoryRepository;
            _supplierRepository = supplierRepository;
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
                    Qty = s.Qty,
                    ImageUrl = s.ImageUrl,
                    // Access string property on Supplier object (with null check default)
                    SupplierName = s.Supplier?.Name ?? "N/A",
                    // Access string property on WarehouseLocation object (with null check default)
                    Location = s.Location?.LocationCode ?? "N/A",
                    CreatedDate = s.CreatedDate.ToString("yyyy-MM-dd")
                }).ToList()
            }).ToList();
        }


        //This Method will Add New Prodcuct and All Four Related models will join here.
        public async Task<ProductResponseDto> CreateProductAsync(CreateProductRequestDto dto)
        {
            // 1. Create new Supplier instance (EF Core auto-generates its Id)
            var newSupplier = new Supplier
            {
                Name = dto.SupplierName,
                GSTNumber = dto.GSTNumber
            };

            var newLocation = new WarehouseLocation
            {
                LocationCode = dto.LocationCode
            };

            var product = new ProductSku
            {
                Sku = dto.Sku,
                MainSku = dto.MainSku,
                CreatedAt = DateTime.UtcNow,
                SubSkus = new List<SubSku>
        {
            new SubSku
            {
                SubSkuCode = dto.SubSkuCode,
                Qty = dto.Qty,
                ImageUrl = dto.ImageUrl,
                Supplier = newSupplier, // Foreign key passed from Supplier UI!
                Location = newLocation,
                CreatedDate = DateTime.UtcNow
            }
        }
            };

            await _inventoryRepository.CreateProductAsync(product);

            // Re-fetch created product with details to return full DTO
            var createdProducts = await _inventoryRepository.GetProductSkusWithDetailsAsync(product.Sku);
            var created = createdProducts.First();

            return new ProductResponseDto
            {
                Id = created.Id,
                Sku = created.Sku,
                MainSku = created.MainSku,
                IsExpanded = false,
                SubSkus = created.SubSkus.Select(s => new SubSkuResponseDto
                {
                    Id = s.Id,
                    SubSku = s.SubSkuCode,
                    Qty = s.Qty,
                    ImageUrl = s.ImageUrl,
                    SupplierName = newSupplier.Name ?? "N/A",
                    Location = s.Location?.LocationCode ?? "N/A",
                    CreatedDate = s.CreatedDate.ToString("yyyy-MM-dd")
                }).ToList()
            };
        }
    }
}
