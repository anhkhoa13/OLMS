using OLMS.Domain.Entities.Quiz;
using OLMS.Domain.Repositories;

public interface IQuizAttemptRepository : IRepository<QuizAttempt>
{
    Task<List<QuizAttempt>> GetAttemptsByStudentId(Guid studentId);
}
