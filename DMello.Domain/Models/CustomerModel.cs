using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DMello.Domain.Models
{
    public class CustomerModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string OrderNo { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string MainSku { get; set; } = string.Empty;

        [MaxLength(50)]
        public string? SubSku { get; set; }

        [MaxLength(20)]
        public string? Size { get; set; }

        [Required]
        [MaxLength(100)]
        public string Customer { get; set; } = string.Empty;

        [MaxLength(500)]
        public string? Description { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
