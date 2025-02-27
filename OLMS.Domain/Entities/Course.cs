using OLMS.Domain.Primitives;

namespace OLMS.Domain.Entities;

public class Course : Entity, IAggregateRoot
{
    public string Title { get; private set; }
    public string Description { get; private set; } = string.Empty;
    public Guid InstructorId { get; private set; }

    // Navigation properties
    public Instructor Instructor { get; private set; }
    private readonly List<Enrollment> _enrollments = new();
    public IReadOnlyCollection<Enrollment> Enrollments => _enrollments.AsReadOnly();

    private Course(Guid id, string title, string description, Instructor instructor) : base(id)
    {
        Title = title;
        Description = description;
        Instructor = instructor;
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
        if (title is null) throw new ArgumentNullException("Title cannot be null");
        if (instructor is null) throw new ArgumentNullException("Instructor cannot be null");

        return new Course(Guid.NewGuid(), title, description, instructor);
    }
}
