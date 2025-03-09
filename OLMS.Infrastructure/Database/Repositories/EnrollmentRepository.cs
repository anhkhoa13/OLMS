using Microsoft.EntityFrameworkCore;
using OLMS.Domain.Entities;
using OLMS.Domain.Repositories;
using OLMS.Infrastructure.Database;


namespace OLMS.Infrastructure.Repositories
{
    public class EnrollmentRepository : IEnrollmentRepository
    {
        private readonly ApplicationDbContext _context;

        public EnrollmentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Enrollment?> GetByStudentAndCourseAsync(Guid studentId, Guid courseId)
        {
            return await _context.Enrollments.FirstOrDefaultAsync(e => e.StudentId == studentId && e.CourseId == courseId);
        }

        public async Task AddAsync(Enrollment enrollment)
        {
            await _context.Enrollments.AddAsync(enrollment);
        }
    }
}
