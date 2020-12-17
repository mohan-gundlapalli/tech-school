using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Reflection;
using System.Threading;

namespace TechSchool.Web.Models
{
    public class SchoolDbContext : DbContext, ISchoolDbContext
    {
        public SchoolDbContext(DbContextOptions<SchoolDbContext> options) : base(options) { }

        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<Subject> Subjects { get; set; }
        public virtual DbSet<StudentSubjectMark> StudentSubjectMarks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Build the models here.
            modelBuilder.Entity<Student>(builder =>
            {
                builder.Property(p => p.FirstName).IsUnicode(false);
                builder.Property(p => p.LastName).IsUnicode(false);
                builder.Property(p => p.City).IsUnicode(false);
                builder.Property(p => p.PhoneNumber).IsUnicode(false);
            });

            modelBuilder.Entity<Subject>(builder =>
            {
                builder.Property(p => p.Name).IsUnicode(false);
                builder.Property(p => p.Description).IsUnicode(false);
            });


            modelBuilder.Entity<StudentSubjectMark>(builder =>
            {
                builder.HasOne(b => b.Student).WithMany();
                builder.HasOne(b => b.Subject).WithMany();
            });
        }

        public new int SaveChanges()
        {
            return base.SaveChanges();
        }

        public new Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
