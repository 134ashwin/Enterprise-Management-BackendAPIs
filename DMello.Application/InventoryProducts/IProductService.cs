using DMello.Application.InventoryProducts.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace DMello.Application.InventoryProducts
{
    // Why it exists: Defines contract methods for fetching and querying product hierarchy data.
    // What problem it solves: Decouples the Web API controller from database logic to make testing easier.
    // How it works: Declares async operations for returning product data filtered by search query.
    public interface IProductService
    {
        Task<List<ProductResponseDto>> GetProductsAsync(string? searchQuery = null);
        Task<ProductResponseDto> CreateProductAsync(CreateProductRequestDto dto);
    }
}
