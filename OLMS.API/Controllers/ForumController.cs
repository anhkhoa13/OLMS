using MediatR;
using Microsoft.AspNetCore.Mvc;
using OLMS.Application.Features.InstructorUC;
using OLMS.Application.Features.CourseUC;

namespace OLMS.Presentation.Controllers;

[ApiController]
[Route("api/forum")]
public class ForumController : ControllerBase {
    private readonly ISender _sender;

    public ForumController(ISender sender) {
        _sender = sender;
    }

    //[HttpGet]
    //public async Task<IActionResult> GetAllForums([FromBody] ) {
    //    var command = new GetCoursesListCommand();
    //    var result = await _sender.Send(command);


    //    return Ok(new { courses, Message = "Courses retrieved successfully" });
    //}


}
