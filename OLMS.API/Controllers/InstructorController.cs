using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OLMS.Application.Features.InstructorUC;
using OLMS.Shared.DTO;

namespace OLMS.API.Controllers;

[ApiController]
//[Authorize(Roles = "Instructor")]
[Route("api/instructor")]
public class InstructorController : Controller
{
    private readonly ISender _sender;
    public InstructorController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost("createcourse")]
    public async Task<IActionResult> CreateCourse([FromBody] CreateCourseCommand command)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new ErrorResponse
            {
                StatusCode = 400,
                Message = "Validation failed",
                Errors = ModelState.ToDictionary(
                    k => k.Key,
                    v => v.Value?.Errors.Select(e => e.ErrorMessage).ToArray() ?? []
                )
            });
        }
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

        var code = result.Value.Value;

        return Ok(new { code, Message = "Course created success" });
    }

    [HttpGet("courses")]
    public async Task<IActionResult> GetCourses([FromQuery] GetAllCoursesCommand command)
    {
        var result = await _sender.Send(command);
        if (!result.IsSuccess || result.Value is null)
        {
            return BadRequest(new
            {
                Code = 400,
                Message = result.Error.ErrorMessage,
                Errors = result.Error.Code
            });
        }
        var courses = result.Value.Select(c => new
        {
            c.Id,
            Code = c.Code.Value,
            c.Title,
            c.Status,
            c.Description
        });
        return Ok(new { courses, Message = "Courses retrieve successful" });
    }
}
