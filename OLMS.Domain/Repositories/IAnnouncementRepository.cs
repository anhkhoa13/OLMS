using OLMS.Domain.Entities.CourseAggregate;
using OLMS.Domain.ValueObjects;

namespace OLMS.Domain.Repositories;

public interface IAnnouncementRepository : IRepository<Announcement>
{
    public Task<List<Announcement>> GetByCourseIdAsync(Guid courseId);
}