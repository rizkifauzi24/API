using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class Employee
    {
        [Key]
        public string NIK { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public DateTime BirdthDate { get; set; }
        public int Salary { get; set; }

        public string Email { get; set; }

        public Gender Gender { get; set; }

        public Account Account { get; set; }

        public bool isDeleted { get; set; }
    }

    public enum Gender
    {
        Male,
        Female
    }

    
}
