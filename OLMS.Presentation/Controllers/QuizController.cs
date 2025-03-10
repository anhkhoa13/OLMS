using MediatR;
using Microsoft.AspNetCore.Mvc;
using OLMS.Application.Feature.Quiz.Command;

namespace OLMS.Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class QuizController : ControllerBase
{
    private readonly ISender _sender; 

    public QuizController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateQuiz([FromBody] CreateMulChoiceQuizCommand command)
    {
        var result = await _sender.Send(command); 
        return Ok(new { QuizId = result });
    }

    [HttpPost("add-question")]
    public async Task<IActionResult> AddQuestion([FromBody] AddMulChoQuesCommand command)
    {
        var result = await _sender.Send(command);
        return Ok(new { QuestionId = result });
    }

    [HttpDelete("remove-question")]
    public async Task<IActionResult> RemoveQuestion([FromBody] RemoveMulChoiceQuizCommand command)
    {
        var result = await _sender.Send(command);
        if (!result) return NotFound("Question not found");

        return Ok("Question removed successfully");
    }

    [HttpPost("attempts/start")]
    public async Task<IActionResult> StartQuizAttempt([FromBody] StartQuizAttemptCommand command)
    {
        var attemptId = await _sender.Send(command);
        return Ok(new { AttemptId = attemptId });
    }
    [HttpPost("attempts/submit")]
    public async Task<IActionResult> SubmitQuiz([FromBody] SubmitQuizCommand command)
    {
        var result = await _sender.Send(command);
        return result ? Ok("Quiz submitted successfully.") : BadRequest("Submission failed.");
    }
}
