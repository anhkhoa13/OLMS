using Microsoft.EntityFrameworkCore;
using OLMS.Domain.Entities;
using OLMS.Domain.Entities.Quiz;
using OLMS.Domain.Repositories;

namespace OLMS.Infrastructure.Database.Repositories;

public class StudentAnswerRepository : Repository<StudentAnswer>, IStudentAnswerRepository
{
    public StudentAnswerRepository(ApplicationDbContext context) : base(context) {}

    public Task AddRangeAsync(IEnumerable<StudentAnswer> answers, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}