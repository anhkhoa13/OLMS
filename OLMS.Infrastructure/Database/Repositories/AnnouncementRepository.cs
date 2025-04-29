using Microsoft.EntityFrameworkCore;
using OLMS.Domain.Entities.CourseAggregate;
using OLMS.Domain.Repositories;
using OLMS.Domain.ValueObjects;

namespace OLMS.Infrastructure.Database.Repositories;

public class AnnouncementRepository(ApplicationDbContext context) : Repository<Announcement>(context), IAnnouncementRepository
{
    public async Task<List<Announcement>> GetByCourseIdAsync(Guid courseId) {
        return await _context.Announcements
            .Where(a => a.CourseId == courseId)
            .OrderByDescending(a => a.CreatedAt)
            .ToListAsync();
    }
}

