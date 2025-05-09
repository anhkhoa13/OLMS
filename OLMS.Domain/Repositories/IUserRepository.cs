﻿using OLMS.Domain.Entities;
using OLMS.Domain.ValueObjects;

namespace OLMS.Domain.Repositories;

public interface IUserRepository : IRepository<UserBase>
{
    // Add user specific methods here
    Task<UserBase?> GetByUsernameAsync(Username username, CancellationToken cancellationToken = default);
    Task<bool> IsUsernameUniqueAsync(Username userName, CancellationToken cancellationToken = default);
    Task<bool> IsEmailUniqueAsync(Email email, CancellationToken cancellationToken = default);
    Task<bool> IsUserExist(Guid userId, CancellationToken cancellation = default);
    Task<List<UserBase>> GetUsersByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken);
}
