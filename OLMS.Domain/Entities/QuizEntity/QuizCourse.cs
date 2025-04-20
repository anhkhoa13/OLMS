namespace OLMS.Domain.Entities.QuizEntity;
public class QuizCourse {
    public Guid QuizId { get; set; }
    public Quiz Quiz { get; set; } = default!;

    public Guid CourseId { get; set; }
    public Course Course { get; set; } = default!;
}
