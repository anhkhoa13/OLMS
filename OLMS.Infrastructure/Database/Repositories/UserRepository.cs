using Microsoft.EntityFrameworkCore;
using OLMS.Domain.Entities;
using OLMS.Domain.Repositories;
using OLMS.Domain.ValueObjects;

namespace OLMS.Infrastructure.Database.Repositories;

public class UserRepository(ApplicationDbContext context) : Repository<UserBase>(context), IUserRepository
{
    //public override async Task<UserBase?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    //{
    //    return await _context.Users.SingleOrDefaultAsync(u => u.Id == id, cancellationToken);
    //}

    public async Task<UserBase?> GetByUsernameAsync(Username username, CancellationToken cancellationToken = default)
    {
        if (username == null)
            throw new ArgumentNullException(nameof(username), "Username cannot be null");

        return await _context.Users.SingleOrDefaultAsync(u => u.Username.Value == username.Value, cancellationToken);
    }

    public async Task<bool> IsEmailUniqueAsync(Email email, CancellationToken cancellationToken = default)
    {
        if(email == null)
            throw new ArgumentNullException(nameof(email), "Email cannot be null");

        return !await _context.Users.AnyAsync(u => u.Email.Value == email.Value, cancellationToken);
    }

    public async Task<bool> IsUserExist(Guid userId, CancellationToken cancellation = default)
    {
        return await _context.Users.AnyAsync(u => u.Id == userId, cancellationToken: cancellation);
    }

    public async Task<bool> IsUsernameUniqueAsync(Username userName, CancellationToken cancellationToken = default)
    {
        if (userName == null)
            throw new ArgumentNullException(nameof(userName), "Username cannot be null");

        return !await _context.Users.AnyAsync(u => u.Username.Value == userName.Value, cancellationToken);
    }

    public Task<List<UserBase>> GetUsersByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken)
    {
        return _context.Users
            .Where(u => ids.Contains(u.Id))
            .ToListAsync(cancellationToken);
    }

}