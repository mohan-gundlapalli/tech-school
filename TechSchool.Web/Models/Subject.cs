using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TechSchool.Web.Models
{
    [Table(nameof(Subject))]
    public class Subject: BaseModel
    {
        [Key]
        public int SubjectId { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(100)]
        public string Description { get; set; }

    }
}
