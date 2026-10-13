namespace DMello.Application.Customers.DTOs
{
    public class CustomerResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string MobileNumber { get; set; } = string.Empty;
        public string? Email { get; set; }
        public string GstNumber { get; set; } = "No GST Number";

        public string address { get; set; }

        public string? description { get; set; }

        public bool IsB2B { get; set; }
    }
}
