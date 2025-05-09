﻿using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OLMS.Application.Features.StudentUC;
using OLMS.Shared.DTO;


namespace OLMS.API.Controllers;

[ApiController]
//[Authorize(Roles = "Student")]
[Route("api/student")]
public class StudentController : Controller
{
    private readonly ISender _sender;
    public StudentController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost("enroll")]
    public async Task<IActionResult> EnrollCourse([FromBody] EnrollCourseCommand command)
    {
        var result = await _sender.Send(command);
        if (!result.IsSuccess)
        {
            return BadRequest(new ErrorResponse
            {
                StatusCode = StatusCodes.Status400BadRequest,
                Message = result.Error.ErrorMessage ?? "Error occured",
                ErrorCode = result.Error.Code
            });
        }
        return Ok(new { Message = "Enrolled course success" });
    }

    [HttpGet("courses")]
    public async Task<IActionResult> GetAllCourses([FromQuery] GetAllCoursesCommand command)
    {
        var result = await _sender.Send(command);
        if (!result.IsSuccess || result.Value is null)
        {
            return BadRequest(new
            {
                Code = 400,
                Message = result.Error.ErrorMessage,
                Errors = result.Error.Code
            });
        }

        var courses = result.Value.Select(c => new
        {
            c.Id,
            Code = c.Code.Value,
            c.Title,
            c.Description,
            Instructor = new {
                Id = c.InstructorId,
                Name = c.Instructor.FullName.Value,
            }
        });

        return Ok(new { courses, Message = "Courses retrieve successful" });
    }

    [HttpPost("submit-exercise")]
    public async Task<IActionResult> SubmitExercise([FromBody] SubmitExerciseCommand command)
    {
        var result = await _sender.Send(command);
        if (!result.IsSuccess)
        {
            return BadRequest(new
            {
                Code = 400,
                Message = result.Error.ErrorMessage,
                Errors = result.Error.Code
            });
        }
        return Ok(new { Message = "Submit exercise success" });
    }

    // Get all things related to student (assignement/quiz deadline, announements) 
    // of every course that he or she enrolls
    [HttpGet("{studentId}/dashboard")]
    public async Task<IActionResult> GetStudentDashboard(Guid studentId) {
        var result = await _sender.Send(new GetDashboardQuery(studentId));

        if (result.IsSuccess) {
            return Ok(result.Value);
        }

        return BadRequest(result.Error);
    }

}

