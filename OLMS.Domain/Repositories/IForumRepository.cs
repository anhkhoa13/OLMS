using OLMS.Domain.Entities.CourseAggregate;
using OLMS.Domain.Entities.ForumAggregate;

namespace OLMS.Domain.Repositories;

public interface IForumRepository : IRepository<Forum>
{
    Task<Forum?> GetForumWithPostsByCourseIdAsync(Guid courseId, CancellationToken cancellationToken);
}