using OLMS.Domain.Primitives;

namespace OLMS.Domain.Entities.ForumAggregate.PostAggregate;

public class Vote : Entity
{
    #region Properties
    public VoteType Type { get; set; } = default!;
    #endregion

    #region Navigations
    public Guid PostId { get; private set; }
    public Guid UserId { get; private set; }
    #endregion

    private Vote() : base() { }

    private Vote(Guid id, VoteType type, Guid postId, Guid userId) : base(id)
    {
        Type = type;
        PostId = postId;
        UserId = userId;
    }

    public static Vote Create(Guid postId, Guid userId, VoteType type)
    {
        return new Vote(Guid.NewGuid(), type, postId, userId);
    }

}
