using Microsoft.EntityFrameworkCore;
using OLMS.Domain.Entities;
using OLMS.Domain.Repositories;
using OLMS.Domain.ValueObjects;

namespace OLMS.Infrastructure.Database.Repositories;

public class UserRepository : Repository<UserBase>, IUserRepository
{
    public UserRepository(ApplicationDbContext context) : base(context) {}

    public override async Task<UserBase?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _context.Users.SingleOrDefaultAsync(u => u.Id == id, cancellationToken);
    }

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

    public async Task<bool> IsInstructor(Guid guid, CancellationToken cancellationToken = default)
    {
        return await _context.Instructors.AnyAsync(i => i.Id == guid);
    }

    public async Task<bool> IsUserExist(Guid userId, CancellationToken cancellation = default)
    {
        return await _context.Users.AnyAsync(u => u.Id == userId);
    }

    public async Task<bool> IsUsernameUniqueAsync(Username userName, CancellationToken cancellationToken = default)
    {
        if (userName == null)
            throw new ArgumentNullException(nameof(userName), "Username cannot be null");

        return !await _context.Users.AnyAsync(u => u.Username.Value == userName.Value, cancellationToken);
    }
}