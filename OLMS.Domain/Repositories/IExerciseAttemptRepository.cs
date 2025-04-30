
using OLMS.Domain.Entities.AssignmentAttempt;

namespace OLMS.Domain.Repositories;

public interface IExerciseAttemptRepository : IRepository<ExerciseAttempt>
{
    Task<ExerciseAttempt?> FindByExerciseIdAndStudentId(Guid exerciseId, Guid studentId, CancellationToken cancellationToken = default);
    Task<List<ExerciseAttempt>> GetAllByExerciseId(Guid exerciseId, CancellationToken cancellationToken = default);
}
