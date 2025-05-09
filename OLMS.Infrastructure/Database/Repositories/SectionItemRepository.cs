using Microsoft.EntityFrameworkCore;
using OLMS.Domain.Entities.CourseAggregate;
using OLMS.Domain.Repositories;
using OLMS.Domain.ValueObjects;

namespace OLMS.Infrastructure.Database.Repositories;

public class SectionItemRepository(ApplicationDbContext context) : Repository<SectionItem>(context), ISectionItemRepository
{
    public async Task<List<SectionItem>> GetBySectionIdAsync(Guid sectionId) {
        return await _context.SectionItems
            .Where(si => si.SectionId == sectionId)
            .OrderBy(si => si.Order)
            .ToListAsync();
    }

    public void UpdateRange(IEnumerable<SectionItem> sectionItems) {
        _context.SectionItems.UpdateRange(sectionItems);
    }
    public async Task<SectionItem?> GetByItemIdAsync(Guid itemId) {
        return await _context.SectionItems
            .FirstOrDefaultAsync(si => si.ItemId == itemId);
    }
    public async Task<List<SectionItem>> GetBySectionIdAndOrderGreaterThanAsync(Guid sectionId, int order) {
        return await _context.SectionItems
            .Where(si => si.SectionId == sectionId && si.Order > order)
            .ToListAsync();
    }


}

