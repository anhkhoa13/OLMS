using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace OLMS.Application.Services;

public interface IAuthService
{
    string GenerateJwt(IReadOnlyCollection<Claim> claims);
    ClaimsPrincipal DecodeJwt(string token);
}
