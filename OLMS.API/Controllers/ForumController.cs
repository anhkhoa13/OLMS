using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OLMS.Application.Features.ForumUC;
using OLMS.Shared.DTO;

namespace OLMS.API.Controllers;

[ApiController]
[Route("api/forum")]
[Authorize]
public class ForumController : ControllerBase
{
    private readonly ISender _sender;

    public ForumController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet]
    public async Task<IActionResult> GetForumDetail([FromQuery] GetForumsDetailCommand command)
    {
        var result = await _sender.Send(command);

        if (!result.IsSuccess || result.Value is null)
        {
            return BadRequest(new ErrorResponse
            {
                StatusCode = StatusCodes.Status400BadRequest,
                Message = result.Error.ErrorMessage ?? "Error occured",
                ErrorCode = result.Error.Code
            });
        }

        return Ok(result.Value);
    }

    [HttpPost("createpost")]
    public async Task<IActionResult> CreatePost([FromBody] CreatePostCommand command)
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

        return Ok(new { Message = "Create post success" });
    }
}