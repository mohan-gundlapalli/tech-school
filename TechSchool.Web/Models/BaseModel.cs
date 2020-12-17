using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TechSchool.Web.Models
{
    public abstract class BaseModel
    {
        public int? CreatedBy { get; set; }
        public int? CreatedTimeUtc { get; set; }
        public int? ModifiedBy { get; set; }
        public int? ModifiedTimeUtc { get; set; }
    }
}
