using Microsoft.EntityFrameworkCore;
using OLMS.Domain.Entities.ProgressAggregate;
using OLMS.Domain.Repositories;

namespace OLMS.Infrastructure.Database.Repositories;

public class ProgressRepository : Repository<Progress>, IProgressRepository
{
    public ProgressRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<IReadOnlyCollection<Progress>> FindProgressesByStudentId(Guid studentId, CancellationToken cancellationToken = default)
    {
        return await _context.Progresses.Where(p => p.StudentId == studentId)
                                        .ToListAsync(cancellationToken);
    }
}
