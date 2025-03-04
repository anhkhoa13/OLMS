using Microsoft.EntityFrameworkCore;
using OLMS.Domain.Entities;
using OLMS.Domain.Repositories;
using OLMS.Domain.ValueObjects;

namespace OLMS.Infrastructure.Database.Repositories;

public class UserRepository : Repository<UserBase>, IUserRepository
{
    public UserRepository(ApplicationDbContext context) : base(context) {}

    public Task<bool> IsEmailUniqueAsync(Email email, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<bool> IsUsernameUniqueAsync(Username userName, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}