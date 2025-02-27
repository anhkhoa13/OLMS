namespace OLMS.Domain.Repositories;

public interface IUnitOfWork
{
    Task SaveChangesAsyncs(CancellationToken cancellationToken = default);
}
