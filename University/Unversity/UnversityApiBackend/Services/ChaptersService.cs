using Microsoft.EntityFrameworkCore;
using UniversityApiBackend.DataAccess;
using UniversityApiBackend.Models.DataModels;

namespace UniversityApiBackend.Services
{
    public class ChaptersService: IChaptersService
    {
        private readonly UniversityDBContext _context;

        public ChaptersService(UniversityDBContext context)
        {
            _context = context;
        }
        public List<Chapter> GetChaptersByCourseId(int courseId)
        {
            return _context.Chapters
                .Where(c => c.CourseId == courseId)
                .ToList();
        }

    }
}
