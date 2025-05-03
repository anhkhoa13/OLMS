using OLMS.Domain.Entities.CourseAggregate;
using OLMS.Domain.ValueObjects;

namespace OLMS.Domain.Repositories;

public interface ICourseRepository : IRepository<Course>
{
    Task<List<Course>> GetCoursesByStudentIdAsync(Guid studentId, CancellationToken cancellationToken);
    public Task<IEnumerable<Course>> GetAllEnrollingCourses();
    Task<Course?> GetByCodeAsync(string code, CancellationToken cancellationToken = default);     
    Task<IReadOnlyCollection<Course>> FindCoursesByInstructorIdAsync(Guid instructorId, CancellationToken cancellationToken = default);
}