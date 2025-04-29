using OLMS.Domain.Entities.ForumAggregate.PostAggregate;

public interface IPostRepository {
    Task<Post> CreatePostAsync(Post post, CancellationToken cancellationToken);
    Task<bool> ForumExistsAsync(Guid forumId, CancellationToken cancellationToken);
}
