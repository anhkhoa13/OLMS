using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OLMS.Application.Features.User;
using OLMS.Domain.Entities;
using OLMS.Domain.Result;

namespace OLMS.API.Controllers;

[ApiController]
[Route("api/user")]
[Authorize]
public class UserController : Controller
{
    protected readonly ISender _sender;

    public UserController(ISender sender)
    {
        _sender = sender; 
    }

    [HttpGet("info")]
    public async Task<IActionResult> GetInfo([FromQuery]GetUserInfoCommand command)
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
        return Ok(new
        {
            result.Value.Id,
            Username = result.Value.Username.Value,
            FullName = result.Value.FullName.Value,
            Email = result.Value.Email.Value,
            result.Value.Age,
            result.Value.Role
        });
    }
}
