using MediatR;
using Microsoft.AspNetCore.Mvc;
using OLMS.Application.Features.LessonUC;

[ApiController]
[Route("api/lesson")]
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
    [HttpGet]
    public async Task<IActionResult> GetLesson(Guid lessonId) {
        var command = new GetLessonQuery(lessonId);
        var result = await _sender.Send(command);

        if (!result.IsSuccess)
            return BadRequest(result.Error);

        return Ok(result.Value);
    }

    [HttpPut("update")]
    public async Task<IActionResult> UpdateLesson([FromBody] UpdateLessonCommand command)
    {
        var result = await _sender.Send(command);
        if (!result.IsSuccess)
            return BadRequest(result.Error);
        return Ok(new { Message = "Update lesson successfully" });
    }

}
