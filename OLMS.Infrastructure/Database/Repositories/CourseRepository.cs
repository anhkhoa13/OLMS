using Microsoft.EntityFrameworkCore;
using OLMS.Domain.Entities;
using OLMS.Domain.Repositories;
using OLMS.Domain.ValueObjects;

namespace OLMS.Infrastructure.Database.Repositories;

public class CourseRepository : Repository<Course>, ICourseRepository
{
    public CourseRepository(ApplicationDbContext context) : base(context) { }

    public async Task<IReadOnlyCollection<Course>> FindCoursesByInstructorIdAsync(Guid instructorId, CancellationToken cancellationToken = default)
    {
        return await _context.Courses.Where(c => c.InstructorId == instructorId)
                                     .ToListAsync(cancellationToken: cancellationToken);
    }

    public async Task<IReadOnlyCollection<Course>> GetAllCourseEnroll(Guid studentId, CancellationToken cancellationToken = default)
    {
        return await _context.Courses.Where(c => c.Enrollments.Any(e => e.StudentId == studentId))
                                     .ToListAsync(cancellationToken: cancellationToken);
    }

    public async Task<Course?> GetByCodeAsync(string code, CancellationToken cancellationToken = default)
    {
        return await _context.Courses.Include(c => c.Enrollments)
                                    .SingleOrDefaultAsync(c => c.Code.Value == code, cancellationToken: cancellationToken);
    }

    public override async Task<Course?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _context.Courses.Include(c => c.Enrollments)
                                     .SingleOrDefaultAsync(c => c.Id == id, cancellationToken);
    }
}

