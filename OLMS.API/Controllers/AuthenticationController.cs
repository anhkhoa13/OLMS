using MediatR;
using Microsoft.AspNetCore.Mvc;
using OLMS.Application.Feature.User;
using OLMS.Application.Services;
using OLMS.Domain.Entities;
using OLMS.Domain.Result;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace OLMS.API.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthenticationController : Controller
{
    private readonly ISender _sender;
    private readonly IAuthService _authenticationService;

    public AuthenticationController(ISender sender, IAuthService authenticationService)
    {
        _sender = sender;
        _authenticationService = authenticationService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterUserCommand command)
    {
        Result<Guid> result = await _sender.Send(command);
        if (!result.IsSuccess)
        {
            return BadRequest(new
            {
                Code = 400,
                Message = result.Error.ErrorMessage,
                Errors = result.Error.Code
            });
        }
        return Ok(new { Guid = result.Value, Message = "User registered successfully" });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginUserCommand command)
    {
        Result<UserBase> result = await _sender.Send(command);

        if (!result.IsSuccess || result.Value is null)
        {
            return BadRequest(new
            {
                Code = 400,
                Message = result.Error.ErrorMessage,
                Errors = result.Error.Code
            });
        }

        var user = result.Value;
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.FullName.Value),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Role, user.Role.ToString())
        };
        var jwt = _authenticationService.GenerateJwt(claims);

        return Ok(new { Token = jwt, Message = "Login Success" });
    }
}
