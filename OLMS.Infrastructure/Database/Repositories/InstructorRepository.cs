using Microsoft.EntityFrameworkCore;
using OLMS.Domain.Entities.CourseAggregate;
using OLMS.Domain.Entities.InstructorAggregate;
using OLMS.Domain.Entities.StudentAggregate;
using OLMS.Domain.Repositories;

namespace OLMS.Infrastructure.Database.Repositories;

public class InstructorRepository(ApplicationDbContext context) :  Repository<Instructor>(context), IInstructorRepository
{
    public override async Task<Instructor?> GetByIdAsync(Guid instructorId, CancellationToken cancellationToken)
    {
        return await _context.Instructors.Include(i => i.Courses)
            .SingleOrDefaultAsync(i => i.Id == instructorId, cancellationToken);
    }
    public async Task<IReadOnlyCollection<Course>> GetAllCourses(Guid instructorId, CancellationToken cancellationToken = default)
    {
        var instructor = await _context.Instructors.Include(i => i.Courses)
            .ThenInclude(c => c.Forum)
            .SingleOrDefaultAsync(i => i.Id == instructorId, cancellationToken);

        if (instructor == null)
        {
            throw new KeyNotFoundException($"Instructor with ID {instructorId} not found.");
        }

        return [.. instructor.Courses];
    }

    public async Task<bool> IsInstructor(Guid guid, CancellationToken cancellationToken = default)
    {
        return await _context.Instructors.AnyAsync(i => i.Id == guid, cancellationToken);
    }
}
