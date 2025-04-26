//using MediatR;
//using Microsoft.AspNetCore.Mvc;
//using OLMS.Application.Feature.CourseUC;
//using OLMS.Application.Features.CourseUC;

//namespace OLMS.Presentation.Controllers;

//[ApiController]
//[Route("api/course")]
//public class CourseController : ControllerBase {
//    private readonly ISender _sender;

//    public CourseController(ISender sender) {
//        _sender = sender;
//    }

//    [HttpPost("create")]
//    public async Task<IActionResult> CreateCourse([FromBody] CreateCourseCommand command) {
//        if (!ModelState.IsValid) {
//            return BadRequest(ModelState);
//        }
//        var result = await _sender.Send(command);
//        if (result.IsFailure) {
//            return BadRequest(result.Error);
//        }
//        // Assuming result.Value is the course code (e.g. "C0AD28")
//        return Ok(new { code = result.Value });
//    }

//    [HttpGet]
//    public async Task<IActionResult> GetAllCourses() {
//        var command = new GetCoursesListCommand();
//        var result = await _sender.Send(command);

//        if (result.IsFailure) {
//            return BadRequest(new {
//                Code = 400,
//                Message = result.Error.ErrorMessage,
//                Errors = result.Error.Code
//            });
//        }

//        var courses = result.Value.Select(c => new {
//            Code = c.Code.Value,
//            c.Title,
//            c.Description,
//            Instructor = new {
//                Id = c.InstructorId,
//                Name = c.Instructor?.FullName,
//            }
//        });

//        return Ok(new { courses, Message = "Courses retrieved successfully" });
//    }
//}
