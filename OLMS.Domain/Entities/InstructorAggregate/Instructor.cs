using OLMS.Domain.Entities.CourseAggregate;
using OLMS.Domain.Primitives;
using OLMS.Domain.ValueObjects;

namespace OLMS.Domain.Entities.InstructorAggregate;

public class Instructor : UserBase
{

    #region Properties
    public string? Department { get; set; }
    #endregion

    #region
    private readonly List<Course> _courses = [];
    public IReadOnlyCollection<Course> Courses => _courses.AsReadOnly();
    #endregion

    private Instructor() : base() { }
    public Instructor(Guid id, Username username, Password password, FullName fullname, Email email, int age) : base(id, username, password, fullname, email, age, Role.Instructor)
    {
    }

    public Course CreateCourse(string title, string description)
    {
        var course = Course.Create(title, description, Id);
        _courses.Add(course);
        return course;
    }
}
