using Azure.Identity;
using Microsoft.EntityFrameworkCore;
using UniversityApiBackend.DataAccess;
using UniversityApiBackend.Models.DataModels;

namespace UniversityApiBackend.Services
{
    public class StudentsService : IStudentsService
    {
        private readonly UniversityDBContext _context;

        public StudentsService(UniversityDBContext context)
        {
            _context = context;
        }
        // TODO: resolve methods


        public IEnumerable<Student> GetStudentsWithCourses()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Student> GetStudentsWithNoCourses()
        {
            throw new NotImplementedException();
        }

        public List<Student> GetStudentsWithoutCourses()
        {
            return _context.Students
                .Where(s => s.Courses.Count == 0)
                .ToList();
        }

        public List<Student> GetStudentsByCourse(int courseId)
        {
            return _context.Students
                .Where(s => s.Courses.Any(c => c.Id == courseId))
                .ToList();
        }
    }
}
