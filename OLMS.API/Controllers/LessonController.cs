using MediatR;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/lessons")]
public class LessonController : ControllerBase {
    private readonly ISender _sender;

    public LessonController(ISender sender) {
        _sender = sender;
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateLesson([FromBody] CreateLessonCommand command) {
        var result = await _sender.Send(command);

        if (!result.IsSuccess)
            return BadRequest(result.Error);

        return Ok(new { Message = "Create lesson successfully" });
    }
}
