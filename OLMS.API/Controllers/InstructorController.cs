using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OLMS.Application.Feature.CourseUC;
using OLMS.Application.Features.Instructor;

namespace OLMS.API.Controllers;

[ApiController]
[Authorize(Roles = "Instructor")]
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
            Code = c.Code.Value,
            c.Title,
            c.Description
        });
        return Ok(new { courses , Message = "Courses retrieve successful"});
    }
}
