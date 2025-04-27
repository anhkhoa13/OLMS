using OLMS.Domain.Entities.ForumAggregate.PostAggregate;
using OLMS.Domain.Primitives;
using System.ComponentModel.DataAnnotations;

namespace OLMS.Domain.Entities.ForumAggregate;

public class Forum : AggregateRoot
{
    #region Properties
    public string Title { get; private set; } = default!;
    #endregion

    #region Navigations
    public Guid CourseId { get; private set; }
    private readonly List<Post> _posts = [];
    public IReadOnlyCollection<Post> Posts => _posts.AsReadOnly();


    #endregion
    private Forum() : base() { }
    private Forum(Guid id, string title, Guid courseId) : base(id)
    {
        Title = title;
        CourseId = courseId;
    }
    public static Forum Create(string title, Guid courseId)
    {
        return new Forum(Guid.NewGuid(), title, courseId);
    }

    public Post CreatePost(string title, string body)
    {
        var post = Post.Create(title, body, Id);
        _posts.Add(post);
        return post;
    }
}
