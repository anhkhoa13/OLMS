namespace OLMS.Domain.Entities;

public class Enrollment
{
    public Guid StudentId { get; set; }
    public Guid CourseId { get; set; }
    public Student Student { get; set; }
    public Course Course { get; set; }

    public Enrollment(Guid studentId, Guid courseId)
    {
        StudentId = studentId;
        CourseId = courseId;
    }
}
