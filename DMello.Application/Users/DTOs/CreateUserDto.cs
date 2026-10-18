using System.ComponentModel.DataAnnotations;

namespace DMello.Application.Users.DTOs
{
    public class CreateUserDto
    {
        [Required(ErrorMessage = "Full Name is required.")]
        [StringLength(150, ErrorMessage = "Name cannot exceed 150 characters.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Mobile Number is required.")]
        [RegularExpression(@"^[0-9+\-\s]{7,20}$", ErrorMessage = "Please enter a valid mobile number.")]
        public string MobileNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email ID is required.")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Role is required.")]
        public string Role { get; set; } = "Employee"; // Admin, Sub-Admin, Employee, Lowest Employee

        [Required(ErrorMessage = "Status is required.")]
        public string Status { get; set; } = "Active"; // Active, Inactive
    }
}
