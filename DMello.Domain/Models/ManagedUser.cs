using System;
using System.ComponentModel.DataAnnotations;

namespace DMello.Domain.Models
{
    /// <summary>
    /// ManagedUser entity represents enterprise team members managed via Dynamic User Management & RBAC.
    /// This is strictly decoupled from the core Authentication/Login User model.
    /// </summary>
    public class ManagedUser
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(150)]
        public string FullName { get; set; } = string.Empty;

        [Required]
        [MaxLength(20)]
        public string MobileNumber { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [MaxLength(150)]
        public string Email { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string Role { get; set; } = "Employee"; // Admin, Sub-Admin, Employee, Lowest Employee

        [Required]
        [MaxLength(20)]
        public string Status { get; set; } = "Active"; // Active, Inactive

        public bool IsDeleted { get; set; } = false;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
