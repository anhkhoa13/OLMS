using OLMS.Domain.Primitives;

namespace OLMS.Domain.Entities.QuizEntity;
public enum QuizAttemptStatus
{
    InProgress,
    Submitted
}
public class QuizAttempt : Entity, IAggregateRoot
{
    public Guid QuizAttempId { get; set; }
    public Guid StudentId { get; private set; }
    public Guid QuizId { get; private set; }
    public List<StudentAnswer> Answers { get; private set; } = new();
    public DateTime? SubmittedAt { get; set; }
    public DateTime StartTime { get; set; }
    public QuizAttemptStatus Status { get; set; }
    public double Score { get; set; }


    public QuizAttempt(Guid id, Guid studentId, Guid quizId, DateTime startTime, QuizAttemptStatus status) : base(id)
    {
        StudentId = studentId;
        QuizId = quizId;
        Status = status;
        StartTime = startTime;
    }
    public void AddAnswer(StudentAnswer answer) => Answers.Add(answer);
}
