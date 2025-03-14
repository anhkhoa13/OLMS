using MediatR;
using Microsoft.AspNetCore.Mvc;
using OLMS.Application.Feature.CourseUC;

namespace OLMS.Presentation.Controllers;

public class CourseController : ControllerBase
{
    private readonly ISender _sender;

    public CourseController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
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
