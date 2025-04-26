using OLMS.Domain.Entities.CourseAggregate;
using OLMS.Domain.Entities.StudentAggregate;
using OLMS.Domain.Primitives;

namespace OLMS.Domain.Entities;

public class Enrollment
{
    public Guid StudentId { get; set; }
    public Guid CourseId { get; set; }
    public Student Student { get; set; } = null!;
    public Course Course { get; set; } = null!;
    private Enrollment() { }

    public Enrollment(Guid studentId, Guid courseId)
    {
        StudentId = studentId;
        CourseId = courseId;
    }
}
