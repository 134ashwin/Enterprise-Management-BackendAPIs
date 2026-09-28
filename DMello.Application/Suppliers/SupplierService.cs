using System;
using System.Collections.Generic;
using System.Text;
using DMello.Application.Suppliers.DTOs;
using DMello.Domain.Models;
using DMello.Domain.Interfaces;

namespace DMello.Application.Suppliers
{
    public class SupplierService : ISupplierService
    {
        private readonly ISupplierRepository _supplierRepository;

        public SupplierService(ISupplierRepository supplierRepository)
        {
            _supplierRepository = supplierRepository;
        }

        public async Task<List<SupplierResponseDto>> GetAllSuppliersAsync()
        {
            var suppliers = await _supplierRepository.GetAllAsync();
            return suppliers.Select(s => new SupplierResponseDto
            {
                Id = s.Id,
                Name = s.Name,
                GSTNumber = s.GSTNumber
            }).ToList();
        }

        public async Task<SupplierResponseDto> CreateSupplierAsync(CreateSupplierRequestDto dto)
        {
            var supplier = new Supplier
            {
                Name = dto.Name,
                GSTNumber = dto.GSTNumber
            };

            var created = await _supplierRepository.CreateAsync(supplier);

            return new SupplierResponseDto
            {
                Id = created.Id,
                Name = created.Name,
                GSTNumber = created.GSTNumber
            };
        }
    }
}
