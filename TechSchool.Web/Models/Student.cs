using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TechSchool.Web.Models
{
    [Table(nameof(Student))]
    [Index(nameof(PhoneNumber), IsUnique = true)]
    public class Student: BaseModel
    {
        [Key]
        public int StudentId { get; set; }

        [StringLength(20)]
        [Column(TypeName = "varchar(20)")]
        public string FirstName { get; set; }

        [Column(TypeName = "varchar(20)")]
        [StringLength(20)]
        public string LastName { get; set; }

        [Column(TypeName = "varchar(50)")]
        [StringLength(50)]
        public string City { get; set; }

        [Column(TypeName = "varchar(20)")]
        [StringLength(20)]
        public string PhoneNumber { get; set; }
    }
}
