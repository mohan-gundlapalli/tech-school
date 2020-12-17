using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechSchool.Web.DTO;
using TechSchool.Web.Infrastructure;
using TechSchool.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace TechSchool.Web.Repositories
{
    public class SchoolRepository : ISchoolRepository
    {
        private ISchoolDbContext _context;
        public SchoolRepository(ISchoolDbContext context) => _context = context;

        public async Task<IList<StudentDTO>> GetStudents()
        {
            var students = await _context.Students.ToListAsync();

            var dto = DataMapper.Map<List<StudentDTO>>(students);

            return dto;
        }

        public async Task<IList<CityDTO>> GetCities(CityQueryDTO query = null)
        {
            var cities = await (from s in _context.Students
                         group s by s.City into gs
                         select (new CityDTO { Name = gs.Key, StudentCount = gs.Count() })).ToListAsync();

            if (query != null)
            {
                cities = cities.Where(c => query.IsMatch(c)).ToList();
            }

            return cities;
        }

        public async Task<StudentDTO> CreateStudent(StudentDTO dto)
        {
            var entity = _context.Students.FirstOrDefault(s => s.PhoneNumber == dto.PhoneNumber);

            if (entity != null)
            {
                throw new InvalidProgramException($"There is a student with the phone number: {dto.PhoneNumber}");
            }

            return await Save(dto);
        }

        public async Task<StudentDTO> Save(StudentDTO dto)
        {
            var entity = _context.Students.FirstOrDefault(s => s.PhoneNumber == dto.PhoneNumber);

            if (entity == null)
            {
                entity = ToModel(dto);
                _context.Students.Add(entity);
            }
            else
            {
                entity = ToModel(dto, entity);
            }

            await _context.SaveChangesAsync();

            dto = ToDTO(entity);
            return dto;
        }

        public async Task<StudentDTO> Delete(int studentId)
        {
            var entity = _context.Students.FirstOrDefault(s => s.StudentId == studentId);

            if (entity == null)
            {
                throw new InvalidOperationException($"There is no student with id: {studentId}");
            }

            _context.Students.Remove(entity);
            await _context.SaveChangesAsync();

            var dto = DataMapper.Map<StudentDTO>(entity);
            return dto;
        }

        private StudentDTO ToDTO(Student student)
        {
            var dto = new StudentDTO
            {
                StudentId = student.StudentId,
                City = student.City,
                FirstName = student.FirstName,
                LastName = student.LastName,
                PhoneNumber = student.PhoneNumber
            };

            return dto;
        }

        private Student ToModel(StudentDTO student)
        {
            Student entity = new();
            entity = ToModel(student, entity);
            return entity;
        }

        private Student ToModel(StudentDTO student, Student entity)
        {
            entity.StudentId = student.StudentId;
            entity.City = student.City;
            entity.FirstName = student.FirstName;
            entity.LastName = student.LastName;
            entity.PhoneNumber = student.PhoneNumber;

            return entity;
        }
    }
}
