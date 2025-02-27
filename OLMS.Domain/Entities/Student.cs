using OLMS.Domain.Primitives;

namespace OLMS.Domain.Entities;

public class Student : UserBase
{
    private readonly List<Enrollment> _enrollments = new();
    public IReadOnlyCollection<Enrollment> Enrollments => _enrollments.AsReadOnly();
    public Student(Guid id, string name, string email, string password) : base(id, name, email, password, Role.Student)
    {
    }
    internal void EnrollInCourse(Enrollment enrollment)
    {
        if (_enrollments.Any(e => e.CourseId == enrollment.CourseId))
            throw new InvalidOperationException("Student is already enrolled in this course.");

        _enrollments.Add(enrollment);
    }
}
