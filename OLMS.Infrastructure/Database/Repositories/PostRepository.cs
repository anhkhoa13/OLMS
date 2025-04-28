

using Microsoft.EntityFrameworkCore;
using OLMS.Domain.Entities.CourseAggregate;
using OLMS.Domain.Entities.ForumAggregate;
using OLMS.Domain.Entities.ForumAggregate.PostAggregate;
using OLMS.Domain.Repositories;
using OLMS.Domain.ValueObjects;

namespace OLMS.Infrastructure.Database.Repositories;

public class PostRepository(ApplicationDbContext context) : Repository<Forum>(context), IPostRepository {
    public async Task<Post> CreatePostAsync(Post post, CancellationToken cancellationToken) {
        await _context.Posts.AddAsync(post, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return post;
    }

    public async Task<bool> ForumExistsAsync(Guid forumId, CancellationToken cancellationToken) {
        return await _context.Forums
            .AnyAsync(f => f.Id == forumId, cancellationToken);
    }
}

