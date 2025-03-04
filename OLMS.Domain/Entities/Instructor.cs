using OLMS.Domain.Primitives;
using OLMS.Domain.ValueObjects;

namespace OLMS.Domain.Entities;

public class Instructor : UserBase
{
    private readonly List<Course> _courses = new();
    public IReadOnlyCollection<Course> Courses => _courses.AsReadOnly();
    public Instructor(Guid id, Username username, Password password, FullName fullname, Email email) : base(id, username, password, fullname, email, Role.Instructor)
    {
    }
    public Course CreateCourse(string title, string description)
    {
        var course = Course.Create(Guid.NewGuid(), title, description, this);
        _courses.Add(course);
        return course;
    }
}
