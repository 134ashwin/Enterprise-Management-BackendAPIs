using System;

namespace DMello.Domain.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string MobileNumber { get; set; } = string.Empty;
        public string? Email { get; set; }
        public string? GstNumber { get; set; }
        public string? address { get; set; }
        public string? description { get; set; }
        public bool IsB2B => !string.IsNullOrWhiteSpace(GstNumber);
    }
}
