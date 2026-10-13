using System.ComponentModel.DataAnnotations;

namespace DMello.Application.Customers.DTOs
{
    public class CustomerCreateDto
    {
        [Required(ErrorMessage = "Customer Name is required")]
        [StringLength(150, ErrorMessage = "Customer Name cannot exceed 150 characters")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Mobile Number is required")]
        [Phone(ErrorMessage = "Invalid Mobile Number format")]
        [StringLength(20, ErrorMessage = "Mobile Number cannot exceed 20 characters")]
        public string MobileNumber { get; set; } = string.Empty;

        [EmailAddress(ErrorMessage = "Invalid Email format")]
        [StringLength(150, ErrorMessage = "Email cannot exceed 150 characters")]
        public string? Email { get; set; }

        [StringLength(50, ErrorMessage = "GST Number cannot exceed 50 characters")]
        public string? GstNumber { get; set; }

        [Required(ErrorMessage = "Address is required")]
        public string address { get; set; } = string.Empty;

        public string? description { get; set; }
    }
}
