using Microsoft.EntityFrameworkCore;
using OLMS.Domain.Entities;
using OLMS.Domain.Entities.QuizEntity;
using OLMS.Domain.Repositories;

namespace OLMS.Infrastructure.Database.Repositories;

public class StudentAnswerRepository : Repository<StudentResponse>, IStudentAnswerRepository
{
    public StudentAnswerRepository(ApplicationDbContext context) : base(context) {}

    public Task AddRangeAsync(IEnumerable<StudentResponse> answers, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}