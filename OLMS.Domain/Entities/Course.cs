using OLMS.Domain.Primitives;
using OLMS.Domain.ValueObjects;

using static OLMS.Domain.Error.Error.Course;

namespace OLMS.Domain.Entities;

public class Course : Entity, IAggregateRoot
{
    public Code Code { get; private set; }
    public string Title { get; private set; }
    public string? Description { get; private set; } = string.Empty;
    public Guid InstructorId { get; private set; }

    // Navigation properties
    public Instructor Instructor { get; private set; }
    private readonly List<Enrollment> _enrollments = new();
    public IReadOnlyCollection<Enrollment> Enrollments => _enrollments.AsReadOnly();

    private Course(Guid id, Code code, string title, string description, Instructor instructor) : base(id)
    {
        Title = title;
        Description = description;
        Instructor = instructor;
        Code = code;
    }
    public void EnrollStudent(Student student)
    {
        if (_enrollments.Any(e => e.StudentId == student.Id))
            throw new InvalidOperationException("Student is already enrolled in this course.");

        _enrollments.Add(new Enrollment(student.Id, Id));
    }
    public static Course Create(Guid id, string title, string description, Instructor instructor)
    {
        // logic khi tạo course
        if (title is null) throw new ArgumentNullException(EmptyTitle);

        if (title.Length < 3 || title.Length > 100) throw new ArgumentException(InvalidTitle);
        if (description.Length > 100) throw new ArgumentException(InvalidDescription);

        if (instructor is null) throw new ArgumentNullException(EmptyInstructor);

        var code = Code.Generate(id);
        return new Course(id, code, title, description, instructor);
    }
}
