using UniversityApiBackend.Models.DataModels;

namespace UniversityApiBackend.Services
{
    public interface ICoursesService
    {
        List<Course> GetCoursesByCategory(string category);
        List<Course> GetCoursesWithoutChapters();
        List<Course> GetCoursesByStudent(string studentName);
    }

}