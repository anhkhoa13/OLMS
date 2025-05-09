using OLMS.Domain.Entities.CourseAggregate;
using OLMS.Domain.ValueObjects;

namespace OLMS.Domain.Repositories;

public interface ISectionItemRepository : IRepository<SectionItem>
{
    Task<List<SectionItem>> GetBySectionIdAsync(Guid sectionId);
    void UpdateRange(IEnumerable<SectionItem> sectionItems);

    Task<SectionItem?> GetByItemIdAsync(Guid itemId);

    Task<List<SectionItem>> GetBySectionIdAndOrderGreaterThanAsync(Guid sectionId, int order);
}