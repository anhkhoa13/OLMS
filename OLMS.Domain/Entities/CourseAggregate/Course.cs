using OLMS.Domain.Entities.ForumAggregate.PostAggregate;
using OLMS.Domain.Entities.StudentAggregate;
using OLMS.Domain.Primitives;
using OLMS.Domain.ValueObjects;

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

    public Guid ForumId { get; private set; }
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
        if (description.Length > 100) throw new ArgumentException("Description must be less than 100 characters", nameof(description));

        Guid id = Guid.NewGuid();

        var code = Code.Generate(id);
        return new Course(id, code, title, description, instructorId, CourseStatus.Enrolling);
    }

    public void AddStudent(Student student)
    {
        if (Status != CourseStatus.Enrolling) 
            throw new ArgumentException("Out of time enrolling");

        if (_students.Contains(student))
            throw new ArgumentException("Student already enroll this course");

        _students.Add(student);
    }

    //public void EnrollStudent(Student student)
    //{
    //    if (_enrollments.Any(e => e.StudentId == student.Id))
    //        throw new InvalidOperationException("Student is already enrolled in this course.");

    //    _enrollments.Add(new Enrollment(student.Id, Id));
    //}
    //public void UploadMaterial(Material material)
    //{
    //    material.MaterialType = MaterialType.CourseContent;
    //    _materialCourse.Add(new MaterialCourse(Id, material.Id));
    //}
}
