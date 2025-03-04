using OLMS.Domain.Entities;
using OLMS.Domain.ValueObjects;

namespace OLMS.Domain.Repositories;

public interface IUserRepository : IRepository<UserBase>
{
    // Add user specific methods here
    Task<bool> IsUsernameUniqueAsync(Username userName, CancellationToken cancellationToken = default);
    Task<bool> IsEmailUniqueAsync(Email email, CancellationToken cancellationToken = default);
}
