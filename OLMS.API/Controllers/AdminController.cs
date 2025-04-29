using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OLMS.Application.Features.AdminUC;
using OLMS.Application.Features.CourseUC;
using OLMS.Shared.DTO;

namespace OLMS.API.Controllers;

[Authorize(Roles = "Admin")]
[Route("api/admin")]
[ApiController]
public class AdminController : Controller
{
    private readonly ISender _sender;
    public AdminController(ISender sender)
    {
        _sender = sender;
    }
    [HttpGet]
    public async Task<IActionResult> GetAllCourse()
    {
        var result = await _sender.Send(new GetCoursesListCommand());
        if (!result.IsSuccess && result.Value is null)
        {
            return BadRequest(new ErrorResponse
            {
                StatusCode = StatusCodes.Status400BadRequest,
                Message = result.Error.ErrorMessage ?? "Error occurred",
                ErrorCode = result.Error.Code
            });
        }

        var courses = result.Value.Select(c => new {
            Code = c.Code.Value,
            c.Title,
            c.Description,
            c.Status,
            Instructor = new
            {
                Id = c.InstructorId,
                Name = c.Instructor?.FullName.Value,
            }
        });

        return Ok(courses);
    }

    [HttpPost("approvecourse")]
    public async Task<IActionResult> ApproveCourse([FromBody] ApproveCourseCommand command)
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
        return Ok(new { Message = "Course approved successfully" });
    }
}
