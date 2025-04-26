using OLMS.Domain.Primitives;

namespace OLMS.Domain.Entities.ForumAggregate.PostAggregate;

public class Comment : Entity
{
    #region Properties
    public string Content { get; private set; } = string.Empty;
    #endregion

    #region Navigations
    public Guid PostId { get; }
    public Guid UserId { get; }
    #endregion

    private Comment() : base() { }
    private Comment(Guid id, string content, Guid postId, Guid userId) : base(id)
    {
        Content = content;
        PostId = postId;
        UserId = userId;
    }
    public static Comment Create(string content, Guid postId, Guid userId)
    {
        return new Comment(Guid.NewGuid(), content, postId, userId);
    }
}
