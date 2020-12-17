using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace TechSchool.Web.Models
{
    public interface ISchoolDbContext
    {
        DbSet<Student> Students { get; set; }
        DbSet<StudentSubjectMark> StudentSubjectMarks { get; set; }
        DbSet<Subject> Subjects { get; set; }
        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}