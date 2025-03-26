using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OLMS.Application.Features.CourseUC;
using OLMS.Application.Features.User;
using OLMS.Domain.Entities;
using OLMS.Domain.Result;
using OLMS.Shared.DTO;

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
            return BadRequest(new ErrorResponse
            {
                StatusCode = StatusCodes.Status400BadRequest,
                Message = result.Error.ErrorMessage ?? "Error occured",
                ErrorCode = result.Error.Code
            });
        }
        return Ok(new
        {
            result.Value.Id,
            Username = result.Value.Username.Value,
            FullName = result.Value.FullName.Value,
            Email = result.Value.Email.Value,
            result.Value.Age,
            result.Value.Role,

            Message = "User info retrieved successfully"
        });
    }

    [HttpPost("upload")]
    public async Task<IActionResult> UploadMaterial([FromForm] UploadMaterialCommand command)
    {
        var result = await _sender.Send(command);
        if (!result.IsSuccess)
        {
            return BadRequest(new ErrorResponse
            {
                StatusCode = StatusCodes.Status400BadRequest,
                Message = result.Error.ErrorMessage ?? "Error occured",
                ErrorCode = result.Error.Code
            });
        }

        return Ok(new { Messsage = "Upload success"});
    }
}
