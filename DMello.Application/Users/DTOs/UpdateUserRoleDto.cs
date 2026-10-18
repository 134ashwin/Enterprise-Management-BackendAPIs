using System.ComponentModel.DataAnnotations;

namespace DMello.Application.Users.DTOs
{
    public class UpdateUserRoleDto
    {
        [Required(ErrorMessage = "Role is required.")]
        public string Role { get; set; } = string.Empty;
    }
}
