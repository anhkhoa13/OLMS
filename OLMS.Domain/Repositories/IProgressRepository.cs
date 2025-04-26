using OLMS.Domain.Entities.ProgressAggregate;

namespace OLMS.Domain.Repositories;

public interface IProgressRepository : IRepository<Progress>
{
    Task<IReadOnlyCollection<Progress>> FindProgressesByStudentId(Guid studentId, CancellationToken cancellationToken = default);
}
