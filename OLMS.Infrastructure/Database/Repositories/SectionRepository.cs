using Microsoft.EntityFrameworkCore;
using OLMS.Domain.Entities.CourseAggregate;
using OLMS.Domain.Entities.SectionEntity;
using OLMS.Domain.Repositories;
using OLMS.Domain.ValueObjects;

namespace OLMS.Infrastructure.Database.Repositories;

public class SectionRepository(ApplicationDbContext context) : Repository<Section>(context), ISectionRepository
{
    public virtual async Task<Section?> GetByIdAsync(Guid id, CancellationToken cancellationToken) {
        return await _context.Sections
            .Include(s => s.SectionItems)
            .SingleOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<Section?> GetSectionById(Guid sectionId) {
        return await _context.Sections
            .Where(s => s.Id == sectionId)
            .Include(s => s.Lessons)
            .Include(s => s.SectionItems)
            .Include(s => s.Assignments)
            .FirstOrDefaultAsync();  
    }

    public async Task<List<Section>> GetSectionsByCourseWithDetailsAsync(Guid courseId) {
        return await _context.Sections
            .Where(s => s.CourseId == courseId)
            .Include(s => s.Lessons)
            .Include(s => s.SectionItems)
            .Include(s => s.Assignments)
            .OrderBy(s => s.Title)  // Or your preferred ordering
            .ToListAsync();
    }

    public async Task<List<Section>> GetByCourseIdAsync(Guid courseId) {
        return await _context.Sections
            .Where(s => s.CourseId == courseId)
            .OrderBy(s => s.Order)  // Important for maintaining order
            .AsNoTracking()  // Recommended for read-only operations
            .ToListAsync();
    }
    public void UpdateRange(IEnumerable<Section> sections) {
        _context.Sections.UpdateRange(sections);
    }
}

