using OLMS.Domain.Primitives;
using OLMS.Domain.ValueObjects;

namespace OLMS.Domain.Entities;

public class Course : Entity, IAggregateRoot
{
    public Code Code { get; private set; } = default!;
    public string Title { get; private set; } = string.Empty;
    public string? Description { get; private set; } = string.Empty;
    public Guid InstructorId { get; private set; }

    // Navigation properties
    public Instructor Instructor { get; private set; } = default!;
    private readonly List<Enrollment> _enrollments = new();
    public IReadOnlyCollection<Enrollment> Enrollments => _enrollments.AsReadOnly();
    private Course() : base() { }

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
        if (string.IsNullOrWhiteSpace(title)) throw new ArgumentNullException(nameof(title), "Title cannot be null");

        if (title.Length < 3 || title.Length > 100) throw new ArgumentException("Title must be 3-100 characters long", nameof(title));
        if (description.Length > 100) throw new ArgumentException("Description must be less than 100 characters", nameof(description));

        if (instructor is null) throw new ArgumentNullException(nameof(instructor), "Missing instructor");

        var code = Code.Generate(id);
        return new Course(id, code, title, description, instructor);
    }
}
