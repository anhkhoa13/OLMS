using MediatR;
using Microsoft.AspNetCore.Mvc;
using OLMS.Application.Feature.CourseUC;

namespace OLMS.Presentation.Controllers;

[Route("/[controller]")]
[ApiController]
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

        return CreatedAtAction(nameof(GetCourseById), new { id = courseId }, new { Id = courseId });
    }


    [HttpGet("{id}")]
    public async Task<IActionResult> GetCourseById(Guid id)
    {
        // This is just a placeholder
        return Ok(new { Id = id, Message = "Course found" });
    }
}
