using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TechSchool.Web.DTO
{
    public class CityQueryDTO
    {
        public string Name { get; set; }
        public int StudentCount { get; set; }

        public bool IsMatch(CityDTO city)
        {
            return (string.IsNullOrEmpty(Name) || Name.Equals(city.Name))
                && (StudentCount <= 0 || city.StudentCount > StudentCount);
        }
    }
}
