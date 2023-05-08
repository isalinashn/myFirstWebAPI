using UniversityApiBackend.Models.DataModels;

namespace UniversityApiBackend.Services
{
    public interface IStudentsService
    {
        IEnumerable<Student> GetStudentsWithCourses();
        IEnumerable<Student> GetStudentsWithNoCourses();
        List<Student> GetStudentsWithoutCourses();
        List<Student> GetStudentsByCourse(int courseId);
    }
}

