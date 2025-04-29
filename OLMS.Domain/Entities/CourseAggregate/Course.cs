using OLMS.Domain.Entities.ForumAggregate;
using OLMS.Domain.Entities.InstructorAggregate;
using OLMS.Domain.Entities.SectionEntity;
using OLMS.Domain.Entities.StudentAggregate;
using OLMS.Domain.Primitives;
using OLMS.Domain.ValueObjects;
using System.Linq;

namespace OLMS.Domain.Entities.CourseAggregate;

public class Course : AggregateRoot
{
    #region Properties
    public Code Code { get; private set; } = default!;
    public string Title { get; private set; } = string.Empty;
    public string? Description { get; set; } = string.Empty;
    public CourseStatus Status { get; private set; }
    #endregion

    #region Navigations
    public Guid InstructorId { get; private set; }
    public Instructor Instructor { get; private set; } = default!;

    private readonly List<Student> _students = [];
    public IReadOnlyCollection<Student> Students => _students.AsReadOnly();
    private readonly List<Section> _sections = [];
    public IReadOnlyCollection<Section> Sections => _sections.AsReadOnly();

    // Announcements collection
    private readonly List<Announcement> _announcements = [];
    public IReadOnlyCollection<Announcement> Announcements => _announcements.AsReadOnly();

    public Forum Forum { get; private set; } = default!;
    #endregion

    private Course() : base() { }

    private Course(Guid id, Code code, string title, string description, Guid instructorId, CourseStatus status) : base(id)
    {
        Title = title;
        Description = description;
        Code = code;
        Status = status;
    }

    public static Course Create(string title, string description, Guid instructorId)
    {
        // logic khi tạo course
        if (string.IsNullOrWhiteSpace(title)) throw new ArgumentNullException(nameof(title), "Title cannot be null");

        if (title.Length < 3 || title.Length > 100) throw new ArgumentException("Title must be 3-100 characters long", nameof(title));
        if (description.Length > 1000) throw new ArgumentException("Description must be less than 100 characters", nameof(description));

        Guid id = Guid.NewGuid();

        var code = Code.Generate(id);

        Course course = new(id, code, title, description, instructorId, CourseStatus.Enrolling);
        course.CreateForum();
        return course;
    }

    public void AddStudent(Student student)
    {
        if (Status != CourseStatus.Enrolling) 
            throw new ArgumentException("Out of time enrolling");

        if (_students.Contains(student))
            throw new ArgumentException("Student already enroll this course");

        _students.Add(student);
    }
    public void AddSection(Section section) {
        _sections.Add(section);
    }

    private void CreateForum()
    {
        if (Forum is not null)
        {
            throw new InvalidOperationException("Forum already created for this course.");
        }

        Forum = Forum.Create(Title + " Forum", Id);
    }

    //public Announcement CreateAnnouncement(string title, string content) {
    //    var announcement = Announcement.CreateAnnouncement(title, content, this.Id);
    //    _announcements.Add(announcement);
    //    return announcement;
    //}

}
