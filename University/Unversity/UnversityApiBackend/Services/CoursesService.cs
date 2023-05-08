using UniversityApiBackend.DataAccess;
using UniversityApiBackend.Models.DataModels;

namespace UniversityApiBackend.Services
{
    public class CoursesService : ICoursesService
    {
        private readonly UniversityDBContext _context;

        public CoursesService(UniversityDBContext context)
        {
            _context = context;
        }

        public List<Course> GetCoursesByCategory(string category)
        {
            return _context.Courses
                .Where(c => c.Categories.Any(cat => cat.Name == category))
                .ToList();
        }

        public List<Course> GetCoursesWithoutChapters()
        {
            return _context.Courses
                .Where(c => c.Chapters.Count == 0)
                .ToList();
        }

        public List<Course> GetCoursesByStudent(string studentName)
        {
            return _context.Courses
                .Where(c => c.Students.Any(s => s.Name == studentName))
                .ToList();
        }
    }

}
