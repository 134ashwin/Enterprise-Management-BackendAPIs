using System;
using System.Collections.Generic;
using System.Text;

namespace DMello.Application.Suppliers.DTOs
{
    public class CreateSupplierRequestDto
    {
        public string Name { get; set; } = string.Empty;
        public string GSTNumber { get; set; } = string.Empty;
    }
}
