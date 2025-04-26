using OLMS.Domain.Entities.CourseAggregate;
using OLMS.Domain.Entities.InstructorAggregate;

namespace OLMS.Domain.Repositories;

public interface IInstructorRepository : IRepository<Instructor>
{
    Task<bool> IsInstructor(Guid guid, CancellationToken cancellationToken = default);
    Task<IReadOnlyCollection<Course>> GetAllCourses(Guid instructorId, CancellationToken cancellationToken = default);
}
