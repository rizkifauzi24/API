using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class Account
    {
        [Key]
        [ForeignKey("NIK")]
        [Required]
        public string NIK { get; set; }

        [Required]
        public string Password { get; set; }

        public string OTP { get; set; }

        public bool isActive { get; set; }

        public DateTime? ExpiredTime { get; set; }

        public Employee Employee { get; set; }

        public Profilling Profilling { get; set; }

        public ICollection<AccountRole> AccountRoles { get; set; }

    }
}
