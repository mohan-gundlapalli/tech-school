using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TechSchool.Web.DTO
{
    public class StudentDTO
    {
        public int StudentId { get; set; }

        [StringLength(20)]
        [Required]
        public string FirstName { get; set; }

        [StringLength(20)]
        [Required]
        public string LastName { get; set; }

        [StringLength(50)]
        [Required]
        public string City { get; set; }

        [StringLength(20)]
        [Required]
        public string PhoneNumber { get; set; }
    }


}
