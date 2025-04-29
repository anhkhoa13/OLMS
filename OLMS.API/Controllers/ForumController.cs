using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OLMS.Application.Features.InstructorUC;
using OLMS.Application.Features.CourseUC;
using OLMS.Application.Features.ForumUC.PostUC;

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

    /// <summary>
    /// Gets a forum with all its posts, comments, and votes by course ID
    /// </summary>
    /// <param name="courseId">The ID of the course</param>
    /// <returns>The forum with all its related data</returns>
    [HttpGet("course/{courseId}")]
    [ProducesResponseType(typeof(ForumDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetForumByCourseId(Guid courseId) {
        var result = await _sender.Send(new GetForumCommand(courseId));

        if (result.IsSuccess) {
            return Ok(result.Value);
        }

        return NotFound(new { message = result.Error });
    }

    /// <summary>
    /// Creates a new post in a forum
    /// </summary>
    /// <param name="command">Post creation details</param>
    /// <returns>Created post details</returns>
    [HttpPost("post/create")]
    [ProducesResponseType(typeof(PostDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreatePost(CreatePostCommand command) {
        var result = await _sender.Send(command);

        if (result.IsSuccess) {
            return CreatedAtAction(
                nameof(CreatePost),
                new { id = result.Value.Id },
                result.Value);
        }

        return BadRequest(new { message = result.Error });
    }

    //[HttpPost("createpost")]
    //public async Task<IActionResult> CreatePost([FromBody] CreatePostCommand command)
    //{
    //    var result = await _sender.Send(command);
    //    if (!result.IsSuccess)
    //    {
    //        return BadRequest(new ErrorResponse
    //        {
    //            StatusCode = StatusCodes.Status400BadRequest,
    //            Message = result.Error.ErrorMessage ?? "Error occured",
    //            ErrorCode = result.Error.Code
    //        });
    //    }

    //    return Ok(new { Message = "Create post success" });
    //}
}