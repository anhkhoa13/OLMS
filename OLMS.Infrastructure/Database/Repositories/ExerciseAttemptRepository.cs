

using Microsoft.EntityFrameworkCore;
using OLMS.Domain.Entities.AssignmentAttempt;
using OLMS.Domain.Repositories;

namespace OLMS.Infrastructure.Database.Repositories;

public class ExerciseAttemptRepository(ApplicationDbContext context) : Repository<ExerciseAttempt>(context), IExerciseAttemptRepository
{
    public async Task<ExerciseAttempt?> FindByExerciseIdAndStudentId(Guid exerciseId, Guid studentId, CancellationToken cancellationToken = default)
    {
        return await _context.ExerciseAttempts
            .Include(e => e.SubmitAttachtment)
            .SingleOrDefaultAsync(e => e.ExerciseId == exerciseId && e.StudentId == studentId, cancellationToken);
    }

    public Task<List<ExerciseAttempt>> GetAllByExerciseId(Guid exerciseId, CancellationToken cancellationToken = default)
    {
        return _context.ExerciseAttempts
            .Include(e => e.SubmitAttachtment)
            .Where(e => e.ExerciseId == exerciseId)
            .ToListAsync(cancellationToken);
    }
}
