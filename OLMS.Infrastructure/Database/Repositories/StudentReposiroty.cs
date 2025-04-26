

using Microsoft.EntityFrameworkCore;
using OLMS.Domain.Entities.CourseAggregate;
using OLMS.Domain.Entities.StudentAggregate;
using OLMS.Domain.Repositories;

namespace OLMS.Infrastructure.Database.Repositories;

public class StudentReposiroty(ApplicationDbContext context) : Repository<Student>(context), IStudentRepository
{
    public async Task<IReadOnlyCollection<Course>> GetAllCourses(Guid studentId, CancellationToken cancellationToken = default)
    {
        var student =  await _context.Students.Include(s => s.Courses)
            .ThenInclude(c => c.Instructor)
            .IgnoreAutoIncludes()
            .SingleOrDefaultAsync(s => s.Id == studentId, cancellationToken);

        if (student == null)
        {
            throw new KeyNotFoundException($"Student with ID {studentId} not found.");
        }

        return student.Courses.ToList();
    }

    public async Task<bool> IsStudent(Guid guid, CancellationToken cancellationToken = default)
    {
        return await _context.Students.AnyAsync(s => s.Id == guid, cancellationToken);
    }


}
