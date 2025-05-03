

using OLMS.Domain.Entities;

namespace OLMS.Domain.Repositories;

public interface IAdminRepository : IRepository<Admin>
{
    Task<bool> IsAdminExist(Guid adminId, CancellationToken cancellation = default);
}
