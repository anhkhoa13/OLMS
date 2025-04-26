using OLMS.Domain.Entities.CourseAggregate;
using OLMS.Domain.Entities.ProgressAggregate;
using OLMS.Domain.Primitives;
using OLMS.Domain.ValueObjects;

namespace OLMS.Domain.Entities.StudentAggregate;

public class Student : UserBase
{
    #region Properties
    public string? Major { get; set; }
    #endregion

    #region Navigations
    private readonly List<Course> _courses = [];
    public IReadOnlyCollection<Course> Courses => _courses.AsReadOnly();

    private readonly List<Progress> _progresses = [];
    public IReadOnlyCollection<Progress> Progresses => _progresses.AsReadOnly();
    #endregion

    private Student() : base() { }
    public Student(Guid id, Username username, Password password, FullName fullname, Email email, int age) : base(id, username, password, fullname, email, age, Role.Student)
    {
    }

    public Progress EnrollCourse(Course course)
    {
        course.AddStudent(this);
        _courses.Add(course);


        var progress = Progress.Create(Guid.NewGuid(), course.Id, Id);
        _progresses.Add(progress);

        return progress;
    }
}
