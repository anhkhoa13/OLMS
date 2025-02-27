using Microsoft.EntityFrameworkCore;
using OLMS.Domain.Entities;
using OLMS.Domain.Repositories;

namespace OLMS.Infrastructure.Database.Repositories;

public class UserRepository : Repository<UserBase>, IUserRepository
{
    public UserRepository(ApplicationDbContext context) : base(context) {}
}