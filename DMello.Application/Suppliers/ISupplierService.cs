using DMello.Application.Suppliers.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace DMello.Application.Suppliers
{
    public interface ISupplierService
    {
        Task<List<SupplierResponseDto>> GetAllSuppliersAsync();
        Task<SupplierResponseDto> CreateSupplierAsync(CreateSupplierRequestDto dto);
    }
}
