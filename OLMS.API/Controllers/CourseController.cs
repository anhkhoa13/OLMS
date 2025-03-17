using MediatR;
using Microsoft.AspNetCore.Mvc;
using OLMS.Application.Feature.CourseUC;
using OLMS.Domain.Entities;
using OLMS.Domain.Repositories;

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

        var courseId = await _sender.Send(command);

        return RedirectToAction("Index");
    }
}
