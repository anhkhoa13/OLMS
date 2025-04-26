

using OLMS.Domain.Entities.CourseAggregate;
using OLMS.Domain.Entities.StudentAggregate;

namespace OLMS.Domain.Repositories;

public interface IStudentRepository : IRepository<Student>
{
    Task<bool> IsStudent(Guid guid, CancellationToken cancellationToken = default);
    Task<IReadOnlyCollection<Course>> GetAllCourses(Guid studentId, CancellationToken cancellationToken = default);
}
