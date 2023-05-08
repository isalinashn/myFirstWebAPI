using UniversityApiBackend.Models.DataModels;

namespace UniversityApiBackend.Services
{
    public interface IChaptersService
    {
        List<Chapter> GetChaptersByCourseId(int courseId);
    }

}
