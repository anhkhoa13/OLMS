using OLMS.Domain.Primitives;

namespace OLMS.Domain.Entities;

public class Instructor : UserBase
{
    private readonly List<Course> _courses = new();
    public IReadOnlyCollection<Course> Courses => _courses.AsReadOnly();
    public Instructor(Guid id, string name, string email, string password) : base(id, name, email, password, Role.Instructor)
    {
    }
    public Course CreateCourse(string title, string description)
    {
        var course = Course.Create(Guid.NewGuid(), title, description, this);
        _courses.Add(course);
        return course;
    }
}
