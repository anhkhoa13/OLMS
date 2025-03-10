using OLMS.Domain.Entities;
using OLMS.Domain.Entities.Quiz;

namespace OLMS.Domain.Repositories;

public interface IStudentAnswerRepository : IRepository<StudentAnswer>
{
    Task AddRangeAsync(IEnumerable<StudentAnswer> answers, CancellationToken cancellationToken);
}
