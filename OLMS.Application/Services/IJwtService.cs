using OLMS.Domain.Entities;

namespace OLMS.Application.Services;

public interface IJwtService
{
    string GenerateToken(UserBase user);
}
