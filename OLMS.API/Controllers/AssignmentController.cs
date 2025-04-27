using MediatR;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/assignment")]
public class AssignmentController : ControllerBase {
    private readonly ISender _sender;

    public AssignmentController(ISender sender) {
        _sender = sender;
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateAssignment([FromBody] CreateExerciseCommand command) {
        var result = await _sender.Send(command);

        if (!result.IsSuccess)
            return BadRequest(result.Error);

        return Ok(new { Message = "Create assignment successfully" });
    }
}
