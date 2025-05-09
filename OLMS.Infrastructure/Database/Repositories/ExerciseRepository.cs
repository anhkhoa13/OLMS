using Microsoft.EntityFrameworkCore;
using OLMS.Domain.Entities.CourseAggregate;
using OLMS.Domain.Repositories;
using OLMS.Domain.ValueObjects;

namespace OLMS.Infrastructure.Database.Repositories;

public class ExerciseRepository(ApplicationDbContext context) : Repository<Exercise>(context), IExerciseRepository
{
    public override async Task<Exercise?> GetByIdAsync(Guid id, CancellationToken cancellationToken) {
        return await _context.Exercises
            .Include(e => e.ExerciseAttachments)
            .FirstOrDefaultAsync(e => e.Id == id);
    }
    public async Task<Exercise?> GetByIdWithAttachmentsAsync(Guid id) {
        return await _context.Exercises
            .Include(e => e.ExerciseAttachments)
            .FirstOrDefaultAsync(e => e.Id == id);
    }
}

