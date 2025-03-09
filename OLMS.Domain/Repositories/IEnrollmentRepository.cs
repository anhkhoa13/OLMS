using OLMS.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace OLMS.Domain.Repositories
{
    public interface IEnrollmentRepository
    {
        Task<Enrollment?> GetByStudentAndCourseAsync(Guid studentId, Guid courseId);
        Task AddAsync(Enrollment enrollment);
    }
}
