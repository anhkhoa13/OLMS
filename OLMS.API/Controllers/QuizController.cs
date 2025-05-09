using MediatR;
using Microsoft.AspNetCore.Mvc;
using OLMS.Application.Features.QuizUC.Command;
using OLMS.Domain.ValueObjects;
using System.Diagnostics;

namespace OLMS.API.Controllers;

[ApiController]
[Route("api/quiz")]
public class QuizController : Controller {
    private readonly ISender _sender;

    public QuizController(ISender sender) {
        _sender = sender;
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateQuiz([FromBody] CreateQuizCommand command) {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var result = await _sender.Send(command);
        if (!result.IsSuccess)
            return BadRequest(result.Error);

        return Ok(new { Message = "Create quiz successfully", QuizId = result.Value });
    }
    [HttpPost("add-questions")]
    public async Task<IActionResult> AddQuestions([FromBody] AddQuestionsCommand command) {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var result = await _sender.Send(command);
        return Ok(new { QuestionId = result });
    }

    [HttpDelete("remove-question")]
    public async Task<IActionResult> RemoveQuestion([FromBody] RemoveQuestionCommand command) {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var result = await _sender.Send(command);
        if (result.IsFailure) {
            // Return an appropriate error response
            return NotFound(result.Error.Code);  // or result.Error.Message depending on your use case
        }

        // Return success if no failure
        return Ok("Question removed successfully");
    }
    [HttpGet("code/{code}")]
    public async Task<IActionResult> GetQuizDetails(string code) {
        if (string.IsNullOrWhiteSpace(code))
            return BadRequest("Code is required");

        var quizDetails = await _sender.Send(new GetQuizDetailsQuery { Code = code });
        if (quizDetails == null) return NotFound("Quiz not found");

        return Ok(quizDetails);
    }
    [HttpPost("attempts/start")]
    public async Task<IActionResult> StartQuizAttempt([FromBody] StartQuizAttemptCommand command) {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var attemptId = await _sender.Send(command);
        return Ok(new { AttemptId = attemptId });
    }
    [HttpPost("attempts/submit")]
    public async Task<IActionResult> SubmitQuiz([FromBody] SubmitQuizCommand command) {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _sender.Send(command);

        if (result.IsFailure)
            return BadRequest(new {
                error = result.Error.ErrorMessage
            });

        return Ok(new {
            message = "Quiz submitted successfully.",
            score = result.Value
        });
    }



    [HttpGet("list")]
    public async Task<IActionResult> GetQuizzes() {
        var quizDetails = await _sender.Send(new GetQuizzesQuery());
        if (quizDetails == null) return NotFound("Quiz not found");

        return Ok(quizDetails);
    }

    // Todo
    [HttpPost("attempts/result")]
    public async Task<IActionResult> ReturnResult() {
        return Ok();
    }

    [HttpPut("update/{quizId}")]
    public async Task<IActionResult> UpdateQuiz(Guid quizId, [FromBody] UpdateQuizCommand command) {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        // Ensure the route quizId matches the command's QuizId
        if (quizId != command.QuizId)
            return BadRequest("Quiz ID mismatch.");

        var result = await _sender.Send(command);
        if (!result.IsSuccess)
            return BadRequest(result.Error);

        return Ok(new { Message = "Quiz updated successfully" });
    }
    [HttpPut("update-questions")]
    public async Task<IActionResult> UpdateQuestions([FromBody] UpdateQuestionsCommand command) {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var result = await _sender.Send(command);
        if (!result.IsSuccess)
            return BadRequest(result.Error);

        return Ok(new {
            Message = "Questions updated successfully",
            UpdatedQuestionIds = result.Value
        });
    }

    [HttpDelete("delete/{quizId}")]
    public async Task<IActionResult> DeleteQuiz(Guid quizId) {
        var command = new DeleteQuizCommand(quizId);
        var result = await _sender.Send(command);

        return result.IsSuccess ? NoContent() : BadRequest(result.Error);
    }




}
