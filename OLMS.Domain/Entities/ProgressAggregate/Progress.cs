using OLMS.Domain.Primitives;

namespace OLMS.Domain.Entities.ProgressAggregate;

public class Progress : AggregateRoot
{
    #region Properties

    #endregion

    #region Navigations
    public Guid CourseId { get; }
    public Guid StudentId { get; }
    #endregion

    private Progress() : base() { }

    private Progress(Guid id, Guid courseId, Guid studentId) : base(id)
    {
        CourseId = courseId;
        StudentId = studentId;
    }

    public static Progress Create(Guid id, Guid courseId, Guid studentId) 
        => new(id, courseId, studentId);

}
