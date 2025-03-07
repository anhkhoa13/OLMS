using MediatR;
using Microsoft.AspNetCore.Mvc;
using OLMS.Application.Feature.CourseUC;
using OLMS.Application.Feature.Quiz;

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
}
