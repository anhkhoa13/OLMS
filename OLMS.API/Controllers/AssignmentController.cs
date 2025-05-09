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

    [HttpGet]
    public async Task<IActionResult> GetAssignment(Guid assignmentId) {
        var command = new GetAssignmentQuery(assignmentId);
        var result = await _sender.Send(command);

        if (!result.IsSuccess)
            return BadRequest(result.Error);

        return Ok(result.Value);
    }

    [HttpDelete("delete/{exerciseId}")]
    public async Task<IActionResult> DeleteExercise(Guid exerciseId) {
        var command = new DeleteExerciseCommand(exerciseId);
        var result = await _sender.Send(command);

        return result.IsSuccess ? NoContent() : BadRequest(result.Error);
    }
}
