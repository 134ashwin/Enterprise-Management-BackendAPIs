using DMello.Application.Customers;
using DMello.Application.Customers.DTOs;
using DMello.Domain.Models;
using DMello.Infrastructure;
using DMello.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DMello.Infrastructure.Repositories
{
    public class CustomerOrderRepository : ICustomerOrderRepository
    {
        private readonly ApplicationDbContext _context;

        public CustomerOrderRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<CustomerOrderResponseDto> CreateAsync(CreateCustomerOrderDto dto)
        {
            var entity = new CustomerModel
            {
                OrderNo = dto.OrderNo,
                MainSku = dto.MainSku,
                SubSku = dto.SubSku,
                Size = dto.Size,
                Customer = dto.Customer,
                Description = dto.Description
            };

            var add_in_sales_part = new SalesOrdersModel // So we want to add the New customer Details in sales part as well as per client request
            {
                OrderNo = dto.OrderNo,
                MainSku = dto.MainSku,
                SubSku = dto.SubSku,
                Size = dto.Size,
                Customer = dto.Customer,
                Description = dto.Description
            };

            _context.CustomerOrders.Add(entity);
            _context.SalesOrders.Add(add_in_sales_part);
            await _context.SaveChangesAsync();

            return MapToDto(entity);
        }

        public async Task<IEnumerable<CustomerOrderResponseDto>> GetAllAsync()
        {
            return await _context.CustomerOrders
                .AsNoTracking()
                .OrderByDescending(x => x.CreatedAt)
                .Select(x => MapToDto(x))
                .ToListAsync();
        }

        public async Task<CustomerOrderResponseDto?> GetByIdAsync(int id)
        {
            var entity = await _context.CustomerOrders.FindAsync(id);
            return entity == null ? null : MapToDto(entity);
        }

        private static CustomerOrderResponseDto MapToDto(CustomerModel entity) =>
            new(
                entity.Id,
                entity.OrderNo,
                entity.MainSku,
                entity.SubSku,
                entity.Size,
                entity.Customer,
                entity.Description,
                entity.CreatedAt
            );
    }
}
