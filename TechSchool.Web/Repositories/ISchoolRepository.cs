using System.Collections.Generic;
using System.Threading.Tasks;
using TechSchool.Web.DTO;

namespace TechSchool.Web.Repositories
{
    public interface ISchoolRepository
    {
        Task<StudentDTO> CreateStudent(StudentDTO dto);
        Task<StudentDTO> Delete(int studentId);
        Task<IList<CityDTO>> GetCities(CityQueryDTO query = null);
        Task<IList<StudentDTO>> GetStudents();
        Task<StudentDTO> Save(StudentDTO dto);
    }
}