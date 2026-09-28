using System;
using System.Collections.Generic;
using System.Text;

namespace DMello.Domain.Models
{
    public class WarehouseLocation
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string LocationCode { get; set; } = string.Empty; // "Aisle 4, Shelf B" written ONCE
    }
}
