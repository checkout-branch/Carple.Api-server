using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Carple.Domain.Enities;

namespace Carple.Domain.Entities
{
    public class Role
    {
        [Key]
        public int RoleId { get; set; }

        [Required]
        [MaxLength(50)]
        public string? RoleName { get; set; }

        public ICollection<User>? Users { get; set; }
    }
}
