using OLMS.Domain.Entities.CourseAggregate;
using OLMS.Domain.ValueObjects;

namespace OLMS.Domain.Repositories;

public interface ILessonRepository : IRepository<Lesson>
{
    public Task<Lesson?> GetLessonById(Guid lessonId);
}