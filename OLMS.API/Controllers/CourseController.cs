using MediatR;
using Microsoft.AspNetCore.Mvc;
using OLMS.Application.Features.InstructorUC;
using OLMS.Application.Features.CourseUC;
using OLMS.Application.Features.CourseUC.SectionUC;
using OLMS.Domain.Entities.CourseAggregate;

namespace OLMS.Presentation.Controllers;

[ApiController]
[Route("api/course")]
public class CourseController : ControllerBase {
    private readonly ISender _sender;

    public CourseController(ISender sender) {
        _sender = sender;
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateCourse([FromBody] CreateCourseCommand command) {
        if (!ModelState.IsValid) {
            return BadRequest(ModelState);
        }
        var result = await _sender.Send(command);
        if (result.IsFailure) {
            return BadRequest(result.Error);
        }
        // Assuming result.Value is the course code (e.g. "C0AD28")
        return Ok(new { code = result.Value });
    }

    [HttpGet]
    public async Task<IActionResult> GetAllCourses() {
        var command = new GetCoursesListCommand();
        var result = await _sender.Send(command);

        if (result.IsFailure) {
            return BadRequest(new {
                Code = 400,
                Message = result.Error.ErrorMessage,
                Errors = result.Error.Code
            });
        }

        var courses = result.Value.Select(c => new {
            Code = c.Code.Value,
            c.Title,
            c.Description,
            Instructor = new {
                Id = c.InstructorId,
                Name = c.Instructor?.FullName.Value,
            }
        });

        return Ok(new { courses, Message = "Courses retrieved successfully" });
    }



    [HttpPost("section/create")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CreateSection(
    Guid courseId,
    [FromBody] CreateSectionCommand request) {
        var result = await _sender.Send(request);

        if (result.IsSuccess) {
            return Ok(new { result.Value, Message = "Section created successfully" });
        }

        return result.Error switch {
            { Code: "Course.NotFound" } => NotFound(new ProblemDetails {
                Title = "Course not found",
                Detail = result.Error.ErrorMessage,
                Status = StatusCodes.Status404NotFound
            }),
            { Code: "Section.DuplicateTitle" } => Conflict(new ProblemDetails {
                Title = "Duplicate section title",
                Detail = result.Error.ErrorMessage,
                Status = StatusCodes.Status409Conflict
            }), { Code: "Section.EmptyTitle" } or
            { Code: "Section.InvalidTitleLength" } => BadRequest(new ProblemDetails {
                Title = "Invalid section title",
                Detail = result.Error.ErrorMessage,
                Status = StatusCodes.Status400BadRequest
            }),
            _ => BadRequest(new ProblemDetails {
                Title = "Invalid request",
                Detail = result.Error.ErrorMessage,
                Status = StatusCodes.Status400BadRequest
            })
        };
    }


}
