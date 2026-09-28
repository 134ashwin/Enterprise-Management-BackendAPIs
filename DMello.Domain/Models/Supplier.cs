using System;
using System.Collections.Generic;
using System.Text;

namespace DMello.Domain.Models
{
    public class Supplier
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty; // "Rajesh Textiles" written ONCE
        public string GSTNumber { get; set; } = string.Empty;
    }
}
