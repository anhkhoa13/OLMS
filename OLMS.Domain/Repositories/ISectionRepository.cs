using OLMS.Domain.Entities.CourseAggregate;
using OLMS.Domain.Entities.SectionEntity;
using OLMS.Domain.ValueObjects;

namespace OLMS.Domain.Repositories;

public interface ISectionRepository : IRepository<Section> {

    public Task<List<Section>> GetSectionsByCourseWithDetailsAsync(Guid courseId);
    public Task<Section> GetSectionById(Guid sectionId);
}