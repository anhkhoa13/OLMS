using OLMS.Domain.Entities;
using OLMS.Domain.Entities.QuizEntity;

namespace OLMS.Domain.Repositories;

public interface IStudentAnswerRepository : IRepository<StudentAnswer>
{
    Task AddRangeAsync(IEnumerable<StudentAnswer> answers, CancellationToken cancellationToken);
}
