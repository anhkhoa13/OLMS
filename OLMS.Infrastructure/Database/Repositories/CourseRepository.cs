using Microsoft.EntityFrameworkCore;
using OLMS.Domain.Entities.CourseAggregate;
using OLMS.Domain.Repositories;
using OLMS.Domain.ValueObjects;

namespace OLMS.Infrastructure.Database.Repositories;

public class CourseRepository(ApplicationDbContext context) : Repository<Course>(context), ICourseRepository
{
    public async Task<IReadOnlyCollection<Course>> FindCoursesByInstructorIdAsync(Guid instructorId, CancellationToken cancellationToken = default)
    {
        return await _context.Courses.Where(c => c.InstructorId == instructorId)
                                     .ToListAsync(cancellationToken);
    }

    public async Task<Course?> GetByCodeAsync(string code, CancellationToken cancellationToken = default)
    {
        return await _context.Courses.SingleOrDefaultAsync(c => c.Code.Value == code, cancellationToken: cancellationToken);
    }

    public override async Task<Course?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _context.Courses.SingleOrDefaultAsync(c => c.Id == id, cancellationToken);
    }
}

