

using Microsoft.EntityFrameworkCore;
using OLMS.Domain.Entities.ForumAggregate.PostAggregate;
using OLMS.Domain.Repositories;

namespace OLMS.Infrastructure.Database.Repositories;

public class PostRepository : Repository<Post>, IPostRepository
{
    public PostRepository(ApplicationDbContext context) : base(context)
    {
    }

    public override async Task<Post?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _context.Posts
            .Include(p => p.Votes)
            .Include(p => p.Comments)
            .SingleOrDefaultAsync(x => x.Id == id, cancellationToken);
    }
}
