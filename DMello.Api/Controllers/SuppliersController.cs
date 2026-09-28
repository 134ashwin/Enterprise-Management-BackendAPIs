using System;
using System.Collections.Generic;
using System.Text;
using DMello.Application.Suppliers.DTOs;
using DMello.Application.Suppliers;
using Microsoft.AspNetCore.Mvc;

namespace DMello.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SuppliersController : ControllerBase
    {
        private readonly ISupplierService _supplierService;

        public SuppliersController(ISupplierService supplierService)
        {
            _supplierService = supplierService;
        }

        [HttpGet]
        public async Task<ActionResult<List<SupplierResponseDto>>> GetSuppliers()
        {
            var suppliers = await _supplierService.GetAllSuppliersAsync();
            return Ok(suppliers);
        }

        [HttpPost]
        public async Task<ActionResult<SupplierResponseDto>> CreateSupplier([FromBody] CreateSupplierRequestDto dto)
        {
            var result = await _supplierService.CreateSupplierAsync(dto);
            return CreatedAtAction(nameof(GetSuppliers), new { id = result.Id }, result);
        }
    }
}
