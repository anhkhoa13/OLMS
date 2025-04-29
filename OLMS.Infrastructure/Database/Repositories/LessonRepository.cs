using Microsoft.EntityFrameworkCore;
using OLMS.Domain.Entities.CourseAggregate;
using OLMS.Domain.Repositories;
using OLMS.Domain.ValueObjects;
using System.Threading;

namespace OLMS.Infrastructure.Database.Repositories;

public class LessonRepository(ApplicationDbContext context) : Repository<Lesson>(context), ILessonRepository
{

    public async Task<Lesson?> GetLessonById(Guid lessonId, CancellationToken cancellationToken) {
        return await _context.Lessons
            .Where(l => l.Id == lessonId)
            .Include(l => l.LessonAttachments)
            .FirstOrDefaultAsync(cancellationToken);
    }
}

