using OLMS.Domain.Primitives;
using OLMS.Domain.ValueObjects;

namespace OLMS.Domain.Entities;

public class Student : UserBase
{
    private readonly List<Enrollment> _enrollments = new();
    public IReadOnlyCollection<Enrollment> Enrollments => _enrollments.AsReadOnly();

    public string? Major { get; set; }
    public DateTime? EnrollmentDate { get; set; }
    private Student() : base() { }
    public Student(Guid id, Username username, Password password, FullName fullname, Email email, int age) : base(id, username, password, fullname, email, age, Role.Student)
    {
    }
    internal void EnrollInCourse(Enrollment enrollment)
    {
        if (_enrollments.Any(e => e.CourseId == enrollment.CourseId))
            throw new InvalidOperationException("Student is already enrolled in this course.");

        _enrollments.Add(enrollment);
    }
    
}
