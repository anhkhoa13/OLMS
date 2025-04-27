using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OLMS.Application.Features.StudentUC;
using OLMS.Shared.DTO;


namespace OLMS.API.Controllers;

[ApiController]
[Authorize(Roles = "Student")]
[Route("api/student")]
public class StudentController : Controller
{
    private readonly ISender _sender;
    public StudentController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost("enroll")]
    public async Task<IActionResult> EnrollCourse([FromBody] EnrollCourseCommand command)
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
        return Ok(new { Message = "Enrolled course success" });
    }

    [HttpGet("courses")]
    public async Task<IActionResult> GetAllCourses([FromQuery] GetAllCoursesCommand command)
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
            c.Description,
            Instructor = new {
                Id = c.InstructorId,
                Name = c.Instructor.FullName.Value,
            }
        });

        return Ok(new { courses, Message = "Courses retrieve successful" });
    }

}

