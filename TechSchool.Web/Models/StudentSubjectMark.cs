using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TechSchool.Web.Models
{
    [Table(nameof(StudentSubjectMark))]
    [Index(nameof(StudentId), nameof(SubjectId), IsUnique = true)]
    public class StudentSubjectMark : BaseModel
    {
        [Key]
        public int StudentSubjectMarkId { get; set; }

        public int StudentId { get; set; }

        public int SubjectId { get; set; }

        public int Marks { get; set; }

        [ForeignKey(nameof(StudentId))]
        public Student Student { get; set; }

        [ForeignKey(nameof(SubjectId))]
        public Subject Subject { get; set; }
    }
}
