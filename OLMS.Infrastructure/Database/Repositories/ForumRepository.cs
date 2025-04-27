

using Microsoft.EntityFrameworkCore;
using OLMS.Domain.Entities.ForumAggregate;
using OLMS.Domain.Repositories;

namespace OLMS.Infrastructure.Database.Repositories;


public class ForumRepository : Repository<Forum>, IForumRepository
{
    public ForumRepository(ApplicationDbContext context) : base(context)
    {
    }

    public override async Task<Forum?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _context.Forums
            .Include(f => f.Posts)
                .ThenInclude(p => p.Votes)
            .Include(f => f.Posts)
                .ThenInclude(p => p.Comments)
            .SingleOrDefaultAsync(f => f.Id == id, cancellationToken);
    }


}
