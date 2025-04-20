using OLMS.Domain.Entities;
using OLMS.Domain.Entities.QuizEntity;

namespace OLMS.Domain.Repositories;

public interface IStudentAnswerRepository : IRepository<StudentResponse>
{
    Task AddRangeAsync(IEnumerable<StudentResponse> answers, CancellationToken cancellationToken);
}
