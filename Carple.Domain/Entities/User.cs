using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carple.Domain.Entities
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        public int RoleId { get; set; }

        [ForeignKey("RoleId")]
        public Role Role { get; set; }

        [MaxLength(100)]
        public string FullName { get; set; }

        [MaxLength(100)]
        [EmailAddress]
        public string Email { get; set; }

        [MaxLength(15)]
        public string PhoneNumber { get; set; }

        [MaxLength(255)]
        public string PasswordHash { get; set; }

        [MaxLength(255)]
        public string ProfileImage { get; set; }

        [MaxLength(50)]
        public string AadhaarNumber { get; set; }

        [MaxLength(50)]
        public string LicenseNumber { get; set; }

        [MaxLength(50)]
        public string Specialization { get; set; }

        public bool? IsApproved { get; set; }
        public bool? IsOnline { get; set; }
        public bool? IsAvailable { get; set; }
        public bool? IsVerified { get; set; }

        [Column(TypeName = "decimal(9,6)")]
        public decimal? Latitude { get; set; }

        [Column(TypeName = "decimal(9,6)")]
        public decimal? Longitude { get; set; }

        [MaxLength(255)]
        public string Address { get; set; }

        [MaxLength(10)]
        public string Pincode { get; set; }

        public DateTime? CreatedAt { get; set; }

        public bool IsBlocked { get; set; }

        public string Apikey { get; set; }  
    }
}
