using OLMS.Domain.Primitives;

namespace OLMS.Domain.Entities;

public class Enrollment : Entity
{
    public Guid StudentId { get; set; }
    public Guid CourseId { get; set; }
    public Student Student { get; set; } = null!;
    public Course Course { get; set; } = null!;
    private Enrollment() : base() { }

    public Enrollment(Guid studentId, Guid courseId)
    {
        StudentId = studentId;
        CourseId = courseId;
    }
}
