using MediatR;
using Microsoft.AspNetCore.Mvc;
using OLMS.Application.Feature.CourseUC;
using OLMS.Domain.Entities;
using OLMS.Domain.Repositories;
using OLMS.Domain.Result;

namespace OLMS.Presentation.Controllers;

[ApiController]
[Route("api/course")]
public class CourseController : ControllerBase
{
    private readonly ISender _sender;

    public CourseController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateCourse([FromBody] CreateCourseCommand command)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var result = await _sender.Send(command);
        if (result.IsFailure) {
            return BadRequest(result.Error);
        }
        // Assuming result.Value is the course code (e.g. "C0AD28")
        return Ok(new { code = result.Value });
    }
}
