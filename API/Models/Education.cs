using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class Education
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public Degree Degree { get; set; }

        [Required]
        public string GPA { get; set; }

        public ICollection<Profilling>  Profilling { get; set; }

        public University University { get; set; }
    }

    public enum Degree
    {
        D3,
        D4,
        S1,
        S2,
        S3
    }
}
