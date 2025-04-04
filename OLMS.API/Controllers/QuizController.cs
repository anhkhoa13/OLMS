﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using OLMS.Application.Features.QuizUC.Command;
using OLMS.Domain.ValueObjects;
using System.Diagnostics;

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
    [HttpPost("add-questions")]
    public async Task<IActionResult> AddQuestions([FromBody] AddQuestionsCommand command)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        
        var result = await _sender.Send(command);
        return Ok(new { QuestionId = result });
    }

    [HttpDelete("remove-question")]
    public async Task<IActionResult> RemoveQuestion([FromBody] RemoveMulChQuesCommand command)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var result = await _sender.Send(command);
        if (!result) return NotFound("Question not found");

        return Ok("Question removed successfully");
    }
    [HttpGet("code/{code}")]
    public async Task<IActionResult> GetQuizDetails(string code)
    {
        if (string.IsNullOrWhiteSpace(code))
            return BadRequest("Code is required");

        var quizDetails = await _sender.Send(new GetQuizDetailsQuery { Code = code });
        if (quizDetails == null) return NotFound("Quiz not found");

        return Ok(quizDetails);
    }
    [HttpPost("attempts/start")]
    public async Task<IActionResult> StartQuizAttempt([FromBody] StartQuizAttemptCommand command)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var attemptId = await _sender.Send(command);
        return Ok(new { AttemptId = attemptId });
    }
    [HttpPost("attempts/submit")]
    public async Task<IActionResult> SubmitQuiz([FromBody] SubmitQuizCommand command)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var result = await _sender.Send(command);
        return result ? Ok("Quiz submitted successfully.") : BadRequest("Submission failed.");
    }

    [HttpGet("list")]
    public async Task<IActionResult> GetQuizzes()
    {
        var quizDetails = await _sender.Send(new GetQuizzesQuery());
        if (quizDetails == null) return NotFound("Quiz not found");

        return Ok(quizDetails);
    }

    // Todo
    [HttpPost("attempts/result")]
    public async Task<IActionResult> ReturnResult()
    {
        return Ok();
    }



}
