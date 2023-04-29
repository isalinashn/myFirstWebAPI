using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Configuration;
using UniversityApiBackend.DataAccess;
using Microsoft.EntityFrameworkCore;
using UniversityApiBackend.Models.DataModels;

namespace LinqSnippets
{
    public class Service
    {
        //Buscar usuarios por email

        static public void findUserByEmail(string email)
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            var connectionString = configuration.GetSection("ConnectionStrings")["UniversityDB"];

            using (var context = new UniversityDBContext(new DbContextOptionsBuilder<UniversityDBContext>()
                .UseSqlServer(connectionString)
                .Options))
            {
                var user = context.Users.FirstOrDefault(u => u.EmailAddress == email);
                if (user != null)
                {
                    // El usuario fue encontrado
                }
                else
                {
                    // El usuario no fue encontrado
                }
            }
        }

        //Buscar alumnos mayores de edad
        static public List<Student> GetMinorStudents()
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            var connectionString = configuration.GetSection("ConnectionStrings")["UniversityDB"];

            using (var context = new UniversityDBContext(new DbContextOptionsBuilder<UniversityDBContext>()
                .UseSqlServer(connectionString)
                .Options))
            {
                var minorStudents = context.Students.Where(s => DateTime.Today.Year - s.Dob.Year < 18).ToList();
                return minorStudents;
            }
        }

        //Buscar alumnos que tengan al menos un curso
        static public List<Student> GetStudentsWithCourse()
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            var connectionString = configuration.GetSection("ConnectionStrings")["UniversityDB"];

            using (var context = new UniversityDBContext(new DbContextOptionsBuilder<UniversityDBContext>()
                .UseSqlServer(connectionString)
                .Options))
            {
                var studentsWithCourse = context.Students.Include(s => s.Courses).Where(s => s.Courses.Count > 0).ToList();
                return studentsWithCourse;
            }
        }

        //Buscar cursos de un nivel determinado que al menos tengan un alumno inscrito
        static public List<Course> FindCoursesByLevelWithStudents(Level level)
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            var connectionString = configuration.GetSection("ConnectionStrings")["UniversityDB"];

            using (var context = new UniversityDBContext(new DbContextOptionsBuilder<UniversityDBContext>()
                .UseSqlServer(connectionString)
                .Options))
            {
                var courses = context.Courses
                    .Include(c => c.Students)
                    .Where(c => c.Level == level && c.Students.Any())
                    .ToList();

                return courses;
            }
        }


        //Buscar cursos de un nivel determinado que sean de una categoría determinada
        static public List<Course> FindCoursesByLevelAndCategory(Level level, string categoryName)
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            var connectionString = configuration.GetSection("ConnectionStrings")["UniversityDB"];

            using (var context = new UniversityDBContext(new DbContextOptionsBuilder<UniversityDBContext>()
                .UseSqlServer(connectionString)
                .Options))
            {
                var courses = context.Courses
                    .Include(c => c.Categories)
                    .Where(c => c.Level == level && c.Categories.Any(cat => cat.Name == categoryName))
                    .ToList();

                return courses;
            }
        }


        //Buscar cursos sin alumnos
        static public List<Course> GetCoursesWithoutStudents()
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            var connectionString = configuration.GetSection("ConnectionStrings")["UniversityDB"];

            using (var context = new UniversityDBContext(new DbContextOptionsBuilder<UniversityDBContext>()
                .UseSqlServer(connectionString)
                .Options))
            {
                var courses = context.Courses.Where(c => c.Students.Count == 0).ToList();
                return courses;
            }
        }
    }
}
