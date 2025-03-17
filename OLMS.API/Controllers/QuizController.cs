using MediatR;
using Microsoft.AspNetCore.Mvc;
using OLMS.Application.Features.QuizUC.Command;

namespace OLMS.API.Controllers;

[ApiController]
[Route("api/quiz")]
public class QuizController : Controller
{
    private readonly ISender _sender; 

    public QuizController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateQuiz([FromBody] CreateQuizCommand command)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var result = await _sender.Send(command); 
        return Ok(new { QuizId = result });
    }
    [HttpPost("add-question")]
    public async Task<IActionResult> AddQuestion([FromBody] AddMulChoQuesCommand command)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        
        var result = await _sender.Send(command);
        return Ok(new { QuestionId = result });
    }

    [HttpDelete("remove-question")]
    public async Task<IActionResult> RemoveQuestion([FromBody] RemoveMulChQuesCommand command)
    {
        var result = await _sender.Send(command);
        if (!result) return NotFound("Question not found");

        return Ok("Question removed successfully");
    }
    [HttpGet("{quizId}")]
    public async Task<IActionResult> GetQuizDetails(Guid quizId)
    {
        var quizDetails = await _sender.Send(new GetQuizDetailsQuery { QuizId = quizId });
        if (quizDetails == null) return NotFound("Quiz not found");

        return Ok(quizDetails);
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


    [HttpPost("attempts/result")]
    public async Task<IActionResult> ReturnResult()
    {
        return Ok();
    }

}
