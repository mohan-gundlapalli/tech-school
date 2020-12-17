using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;
using TechSchool.Web.Api;
using TechSchool.Web.DTO;
using TechSchool.Web.Models;
using TechSchool.Web.Repositories;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using TechSchool.Tests.Infrastructure;
using TechSchool.Web.Infrastructure;
using System;

namespace TechSchool.Tests
{
    [TestFixture]
    public class Tests
    {
        
        [SetUp]
        public void Setup()
        {
            DataMapper.InitMapper();
        }

        [Test]
        public async Task Can_Get_Students()
        {
            // Arrange
            var students = new List<StudentDTO>
            {
                new StudentDTO{City = "City1", FirstName="FirstName1", LastName = "LastName1", PhoneNumber = "PhoneNumber1", StudentId = 1},
                new StudentDTO{City = "City2", FirstName="FirstName2", LastName = "LastName2", PhoneNumber = "PhoneNumber2", StudentId = 2},
                new StudentDTO{City = "City3", FirstName="FirstName3", LastName = "LastName3", PhoneNumber = "PhoneNumber3", StudentId = 3},
                new StudentDTO{City = "City4", FirstName="FirstName4", LastName = "LastName4", PhoneNumber = "PhoneNumber4", StudentId = 4},
            };
            var schoolRepo = new Mock<ISchoolRepository>();
            schoolRepo.Setup(m => m.GetStudents()).ReturnsAsync(students);

            var logger = new Mock<ILogger<SchoolApiController>>();

            var controller = new SchoolApiController(logger.Object, schoolRepo.Object);

            var result = await controller.Students();
            var apiResult = result.Value;

            Assert.True(apiResult.Success);
            Assert.True(apiResult.Data is IList<StudentDTO>);

            var returnedStudents = apiResult.Data as IList<StudentDTO>;
            Assert.AreEqual(returnedStudents.Count, students.Count);

            Assert.AreEqual(returnedStudents[0].FirstName, students[0].FirstName);

        }

        [Test]
        public async Task Can_Create_Student()
        {
            // Arrange
            var studentModels = new List<Student>
            {
                new Student{City = "City1", FirstName="FirstName1", LastName = "LastName1", PhoneNumber = "PhoneNumber1", StudentId = 1},
                new Student{City = "City2", FirstName="FirstName2", LastName = "LastName2", PhoneNumber = "PhoneNumber2", StudentId = 2},
                new Student{City = "City3", FirstName="FirstName3", LastName = "LastName3", PhoneNumber = "PhoneNumber3", StudentId = 3},
                new Student{City = "City4", FirstName="FirstName4", LastName = "LastName4", PhoneNumber = "PhoneNumber4", StudentId = 4},
            };

            DbSet<Student> dbSet = GetDbSet<Student>(studentModels.AsQueryable()).Object;


            var studentInput = new StudentDTO { FirstName = "FirstName", LastName = "LastName", PhoneNumber= "PhoneNumber", City="City" };

            var context = new Mock<ISchoolDbContext>();
            context.Setup(m => m.Students).Returns(dbSet);

            var repo = new SchoolRepository(context.Object);

            
            var result = await repo.CreateStudent(studentInput);

            context.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);

        }

        [Test]
        public void Cannot_Create_Student_With_Existing_Phone_Number()
        {
            // Arrange
            var studentModels = new List<Student>
            {
                new Student{City = "City1", FirstName="FirstName1", LastName = "LastName1", PhoneNumber = "PhoneNumber1", StudentId = 1},
                new Student{City = "City2", FirstName="FirstName2", LastName = "LastName2", PhoneNumber = "PhoneNumber2", StudentId = 2},
                new Student{City = "City3", FirstName="FirstName3", LastName = "LastName3", PhoneNumber = "PhoneNumber3", StudentId = 3},
                new Student{City = "City4", FirstName="FirstName4", LastName = "LastName4", PhoneNumber = "PhoneNumber4", StudentId = 4},
            };

            DbSet<Student> dbSet = GetDbSet<Student>(studentModels.AsQueryable()).Object;


            var studentInput = new StudentDTO { FirstName = "FirstName", LastName = "LastName", PhoneNumber = "PhoneNumber1", City = "City" };

            var context = new Mock<ISchoolDbContext>();
            context.Setup(m => m.Students).Returns(dbSet);

            var repo = new SchoolRepository(context.Object);


            Assert.ThrowsAsync(typeof(InvalidProgramException), async () => await repo.CreateStudent(studentInput));

        }

        private static DbSet<T> GetQueryableMockDbSet<T>(List<T> sourceList) where T : class
        {
            var queryable = sourceList.AsQueryable();

            var dbSet = new Mock<DbSet<T>>();
            dbSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryable.Provider);
            dbSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryable.Provider);
            dbSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryable.Expression);
            dbSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
            dbSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(() => queryable.GetEnumerator());
            dbSet.Setup(d => d.Add(It.IsAny<T>())).Callback<T>((s) => sourceList.Add(s));

            return dbSet.Object;
        }

        // Return a DbSet of the specified generic type with support for async operations
        public static Mock<DbSet<T>> GetDbSet<T>(IQueryable<T> TestData) where T : class
        {
            var MockSet = new Mock<DbSet<T>>();
            MockSet.As<IAsyncEnumerable<T>>().Setup(x => x.GetAsyncEnumerator(It.IsAny<CancellationToken>())).Returns(new TestAsyncEnumerator<T>(TestData.GetEnumerator()));
            MockSet.As<IQueryable<T>>().Setup(x => x.Provider).Returns(new TestAsyncQueryProvider<T>(TestData.Provider));
            MockSet.As<IQueryable<T>>().Setup(x => x.Expression).Returns(TestData.Expression);
            MockSet.As<IQueryable<T>>().Setup(x => x.ElementType).Returns(TestData.ElementType);
            MockSet.As<IQueryable<T>>().Setup(x => x.GetEnumerator()).Returns(TestData.GetEnumerator());
            return MockSet;
        }
    }
}