

using Microsoft.EntityFrameworkCore;
using OLMS.Domain.Entities.CourseAggregate;
using OLMS.Domain.Entities.ForumAggregate;
using OLMS.Domain.Repositories;
using OLMS.Domain.ValueObjects;

namespace OLMS.Infrastructure.Database.Repositories;

public class ForumRepository(ApplicationDbContext context) : Repository<Forum>(context), IForumRepository
{
    public async Task<Forum?> GetForumWithPostsByCourseIdAsync(Guid courseId, CancellationToken cancellationToken) {
        return await _context.Forums
            .Include(f => f.Posts)
                .ThenInclude(p => p.Votes)
            .Include(f => f.Posts)
                .ThenInclude(p => p.Comments)
            .Include(f => f.Posts)
                .ThenInclude(p => p.Votes)
            .FirstOrDefaultAsync(f => f.CourseId == courseId, cancellationToken);
    }
}


}
