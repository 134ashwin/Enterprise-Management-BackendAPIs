using DMello.Application.InventoryProducts;
using DMello.Application.InventoryProducts.DTOs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace DMello.Api.Controllers
{
    // Why it exists: Exposes RESTful HTTP endpoints for fetching product inventory hierarchy.
    // What problem it solves: Handles HTTP requests, passes query parameters to services, and returns HTTP status responses.
    // How it works: Routes `GET api/products?query=...` requests directly to `IProductService`.

    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<List<ProductResponseDto>>> GetProducts([FromQuery] string? query)
        {
            var products = await _productService.GetProductsAsync(query);
            return Ok(products);
        }

        [HttpPost]
        public async Task<ActionResult<ProductResponseDto>> CreateProduct(CreateProductRequestDto dto)
        {
            var result = await _productService.CreateProductAsync(dto);
            return Ok(result);
        }
    }
}
