using OLMS.Domain.Primitives;
using System.ComponentModel.DataAnnotations;

namespace OLMS.Domain.Entities.ForumAggregate.PostAggregate;

public class Post : AggregateRoot
{
    #region Properties
    public string Title { get; } = default!;
    public string Body { get; } = default!;
    public int VoteScore
    {
        get
        {
            return _votes.Count(v => v.Type == VoteType.UpVote)
                 - _votes.Count(v => v.Type == VoteType.DownVote);
        }
        private set;
    }
    #endregion

    #region Navigations

    public Guid ForumId { get; }
    private readonly List<Vote> _votes = [];

    public IReadOnlyCollection<Vote> Votes => _votes.AsReadOnly();

    private readonly List<Comment> _comments = [];
    public IReadOnlyCollection<Comment> Comments => _comments.AsReadOnly();
    #endregion

    private Post() : base() { }

    private Post(Guid id, string title, string body, Guid forumId) : base(id)
    {
        Title = title;
        Body = body;
        ForumId = forumId;
    }

    public static Post Create(string title, string body, Guid forumId)
    {
        return new Post(Guid.NewGuid(), title, body, forumId);
    }

    public Vote? UpVote(Guid userId)
    {
        var existingVote = _votes.FirstOrDefault(v => v.UserId == userId);

        if (existingVote is not null)
        {
            if (existingVote.Type == VoteType.UpVote)
                throw new InvalidOperationException("User has already upvoted.");

            existingVote.Type = VoteType.UpVote;
            return null;
        }
        else
        {
            var vote = Vote.Create(Id, userId, VoteType.UpVote);
            _votes.Add(vote);

            return vote;
        }
    }
    public Vote? DownVote(Guid userId)
    {
        var existingVote = _votes.FirstOrDefault(v => v.UserId == userId);
        if (existingVote is not null)
        {
            if (existingVote.Type == VoteType.DownVote)
                throw new InvalidOperationException("User has already downvoted.");
            existingVote.Type = VoteType.DownVote;
            return null;
        }
        else
        {
            var vote = Vote.Create(Id, userId, VoteType.DownVote);
            _votes.Add(vote);
            return vote;
        }
    }
    public void UnVote(Guid userId)
    {
        var vote = _votes.FirstOrDefault(v => v.UserId == userId);
        if (vote is not null)
        {
            _votes.Remove(vote);
        }
        else
        {
            throw new InvalidOperationException("User has not voted yet.");
        }
    }

    public Comment AddComment(string content, Guid userId)
    {
        Comment comment = Comment.Create(content, Id, userId);
        _comments.Add(comment);
        return comment;
    }
}
