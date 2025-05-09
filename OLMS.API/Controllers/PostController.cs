using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OLMS.Application.Features.PostUC;
using OLMS.Shared.DTO;


namespace OLMS.API.Controllers;

[ApiController]
[Route("api/post")]
[Authorize]
public class PostController(ISender sender) : Controller
{
    private readonly ISender _sender = sender;

    [HttpPost("upvote")]
    public async Task<IActionResult> UpvotePost([FromBody] UpvotePostCommand command)
    {
        var result = await _sender.Send(command);
        if (!result.IsSuccess)
        {
            return BadRequest(new ErrorResponse
            {
                StatusCode = StatusCodes.Status400BadRequest,
                Message = result.Error.ErrorMessage ?? "Error occurred",
                ErrorCode = result.Error.Code
            });
        }
        return Ok(new { Message = "Upvote success" });
    }
    [HttpPost("downvote")]
    public async Task<IActionResult> DownvotePost([FromBody] DownvotePostCommand command)
    {
        var result = await _sender.Send(command);
        if (!result.IsSuccess)
        {
            return BadRequest(new ErrorResponse
            {
                StatusCode = StatusCodes.Status400BadRequest,
                Message = result.Error.ErrorMessage ?? "Error occurred",
                ErrorCode = result.Error.Code
            });
        }
        return Ok(new { Message = "Downvote success" });
    }

    [HttpPost("unvote")]
    public async Task<IActionResult> UnvotePost([FromBody] UnvotePost command)
    {
        var result = await _sender.Send(command);
        if (!result.IsSuccess)
        {
            return BadRequest(new ErrorResponse
            {
                StatusCode = StatusCodes.Status400BadRequest,
                Message = result.Error.ErrorMessage ?? "Error occurred",
                ErrorCode = result.Error.Code
            });
        }
        return Ok(new { Message = "Unvote success" });
    }

    [HttpPost("addcomment")]
    public async Task<IActionResult> AddComment([FromBody] AddCommentCommand command)
    {
        var result = await _sender.Send(command);
        if (!result.IsSuccess)
        {
            return BadRequest(new ErrorResponse
            {
                StatusCode = StatusCodes.Status400BadRequest,
                Message = result.Error.ErrorMessage ?? "Error occurred",
                ErrorCode = result.Error.Code
            });
        }
        return Ok(new { Message = "Comment added" });
    }

    [HttpGet("getpost")]
    public async Task<IActionResult> GetPost([FromQuery] GetPostDetailCommand command)
    {
        var result = await _sender.Send(command);
        if (!result.IsSuccess)
        {
            return BadRequest(new ErrorResponse
            {
                StatusCode = StatusCodes.Status400BadRequest,
                Message = result.Error.ErrorMessage ?? "Error occurred",
                ErrorCode = result.Error.Code
            });
        }
        return Ok(result.Value);
    }

    [HttpDelete("delete/{postId}")]
    public async Task<IActionResult> DeletePost(Guid postId) {
        var command = new DeletePostCommand(postId);
        var result = await _sender.Send(command);

        return result.IsSuccess ? NoContent() : BadRequest(result.Error);
    }
}
