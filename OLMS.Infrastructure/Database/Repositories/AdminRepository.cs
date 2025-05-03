
using Microsoft.EntityFrameworkCore;
using OLMS.Domain.Entities;
using OLMS.Domain.Repositories;

namespace OLMS.Infrastructure.Database.Repositories;

public class AdminRepository : Repository<Admin>, IAdminRepository
{
    public AdminRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<bool> IsAdminExist(Guid adminId, CancellationToken cancellation = default)
    {
        return await _context.Admins.AnyAsync(a => a.Id == adminId, cancellation);
    }
}
