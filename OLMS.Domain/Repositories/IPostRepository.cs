using OLMS.Domain.Entities.ForumAggregate.PostAggregate;
using OLMS.Domain.Repositories;

public interface IPostRepository : IRepository<Post> {
    Task<Post> CreatePostAsync(Post post, CancellationToken cancellationToken);
    Task<bool> ForumExistsAsync(Guid forumId, CancellationToken cancellationToken);
}
