using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Reflection.Emit;
using workshop_1.Models;
using workshop_1.Data;

namespace workshop_1.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // DbSets (Tables)
        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }

        // Fluent API
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // COURSE → FIRST TEACHER (one-to-many)
            modelBuilder.Entity<Course>()
                .HasOne(c => c.FirstTeacher) //course ima EDEN first teacher
                .WithMany(t => t.FirstTeacherCourses) // teacher ima MNOGU first teacher courses
                .HasForeignKey(c => c.FirstTeacherId) //foreign key vo course
                .OnDelete(DeleteBehavior.Restrict);

            // COURSE → SECOND TEACHER (one-to-many)
            modelBuilder.Entity<Course>()
                .HasOne(c => c.SecondTeacher)
                .WithMany(t => t.SecondTeacherCourses)
                .HasForeignKey(c => c.SecondTeacherId)
                .OnDelete(DeleteBehavior.Restrict);

            // ENROLLMENT → STUDENT (many-to-one)
          
            modelBuilder.Entity<Enrollment>()
                .HasOne(e => e.Student) // enrollments ima EDEN student
                .WithMany(s => s.Enrollments) // student ima MNOGU enrollments
                .HasForeignKey(e => e.StudentId)
                .OnDelete(DeleteBehavior.Cascade);

            // ENROLLMENT → COURSE (many-to-one)
            modelBuilder.Entity<Enrollment>()
                .HasOne(e => e.Course) // enrollment ima EDEN course
                .WithMany(c => c.Enrollments) // course ima MNOGU enrollments
                .HasForeignKey(e => e.CourseId)
                .OnDelete(DeleteBehavior.Cascade);

            // OPTIONAL: UNIQUE CONSTRAINT
            // (еден студент да не се запише двапати на ист предмет)
            modelBuilder.Entity<Enrollment>()
    .HasIndex(e => new { e.StudentId, e.CourseId, e.Year, e.Semester })
    .IsUnique();


            modelBuilder.Entity<ApplicationUser>()
                .HasOne(u => u.Student)
                .WithOne()
                .HasForeignKey<ApplicationUser>(u => u.StudentId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ApplicationUser>()
                .HasOne(u => u.Teacher)
                .WithOne()
                .HasForeignKey<ApplicationUser>(u => u.TeacherId)
                .OnDelete(DeleteBehavior.Restrict);


            // SEED DATA

            // ---------- TEACHERS ----------
            modelBuilder.Entity<Teacher>().HasData(
                new Teacher { Id = 1, FirstName = "Ivan", LastName = "Petrov", Degree = "PhD", AcademicRank = "Professor" },
                new Teacher { Id = 2, FirstName = "Ana", LastName = "Stojanova", Degree = "PhD", AcademicRank = "Associate Professor" },
                new Teacher { Id = 3, FirstName = "Marko", LastName = "Iliev", Degree = "MSc", AcademicRank = "Assistant" },
                new Teacher { Id = 4, FirstName = "Elena", LastName = "Nikolova", Degree = "PhD", AcademicRank = "Professor" },
                new Teacher { Id = 5, FirstName = "Petar", LastName = "Kostov", Degree = "MSc", AcademicRank = "Assistant" }
            );

            // ---------- STUDENTS ----------
            modelBuilder.Entity<Student>().HasData(
                new Student { Id = 1, StudentId = "201001", FirstName = "Stefan", LastName = "Mitrev", CurrentSemester = 5, EducationLevel = "Undergraduate" },
                new Student { Id = 2, StudentId = "201002", FirstName = "Marija", LastName = "Trajanova", CurrentSemester = 3, EducationLevel = "Undergraduate" },
                new Student { Id = 3, StudentId = "201003", FirstName = "Nikola", LastName = "Petreski", CurrentSemester = 7, EducationLevel = "Undergraduate" },
                new Student { Id = 4, StudentId = "201004", FirstName = "Sara", LastName = "Georgieva", CurrentSemester = 1, EducationLevel = "Undergraduate" },
                new Student { Id = 5, StudentId = "201005", FirstName = "Daniel", LastName = "Ristov", CurrentSemester = 5, EducationLevel = "Undergraduate" }
            );

            // ---------- COURSES ----------
            modelBuilder.Entity<Course>().HasData(
                new Course { Id = 1, Title = "Databases", Credits = 6, Semester = 4, Programme = "Software Engineering", FirstTeacherId = 1, SecondTeacherId = 2 },
                new Course { Id = 2, Title = "Web Programming", Credits = 6, Semester = 5, Programme = "Software Engineering", FirstTeacherId = 2, SecondTeacherId = 3 },
                new Course { Id = 3, Title = "Algorithms", Credits = 6, Semester = 3, Programme = "Computer Science", FirstTeacherId = 1, SecondTeacherId = 4 },
                new Course { Id = 4, Title = "Operating Systems", Credits = 6, Semester = 4, Programme = "Computer Science", FirstTeacherId = 4, SecondTeacherId = 5 },
                new Course { Id = 5, Title = "Software Design", Credits = 6, Semester = 6, Programme = "Software Engineering", FirstTeacherId = 1, SecondTeacherId = 2 }
            );

            // ---------- ENROLLMENTS ----------
            modelBuilder.Entity<Enrollment>().HasData(
                new Enrollment { Id = 1, StudentId = 1, CourseId = 1, Year = 2024, Semester = "Spring", Grade = 8 },
                new Enrollment { Id = 2, StudentId = 1, CourseId = 2, Year = 2024, Semester = "Fall", Grade = 9 },
                new Enrollment { Id = 3, StudentId = 2, CourseId = 1, Year = 2024, Semester = "Spring", Grade = 10 },
                new Enrollment { Id = 4, StudentId = 3, CourseId = 3, Year = 2024, Semester = "Fall", Grade = 7 },
                new Enrollment { Id = 5, StudentId = 4, CourseId = 4, Year = 2024, Semester = "Spring", Grade = 8 }
            );

        }
    }
}
