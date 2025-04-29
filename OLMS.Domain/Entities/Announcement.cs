using OLMS.Domain.Entities.CourseAggregate;
using OLMS.Domain.Primitives;
public class Announcement : Entity {
    public string Title { get; private set; }
    public string Content { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public Guid CourseId { get; private set; }

    // Navigation property
    //public Course Course { get; private set; }

    // Private constructor for EF Core
    private Announcement() { }
    public Announcement(Guid id, string title, string content, Guid courseId) : base(id) {
        Title = title;
        Content = content;
        CreatedAt = DateTime.Now;
        CourseId = courseId;
    }

    // Factory method as shown in UML
    public static Announcement CreateAnnouncement(string title, string content, Guid courseId) {
        return new Announcement (Guid.NewGuid(), title, content, courseId);
    }
}

