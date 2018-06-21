using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Questudo.Models;
using Microsoft.EntityFrameworkCore;

namespace Questudo.Data
{
    public class ClassroomContext : DbContext
    {
        public ClassroomContext(DbContextOptions<ClassroomContext> options) : base(options)
        {
        }

        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Classroom> Classrooms { get; set; }
        public DbSet<Enrolled> Enrolleds { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<Student>().ToTable("Student");
            modelBuilder.Entity<Instructor>().ToTable("Instructor");
            modelBuilder.Entity<Classroom>().ToTable("Classroom");
            modelBuilder.Entity<Enrolled>().ToTable("Enrolled");
        }
    }
}
