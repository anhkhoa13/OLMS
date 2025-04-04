﻿using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OLMS.Application.Features.StudentUC;

namespace OLMS.API.Controllers;

public class EnrollmentController : ControllerBase
{
    private readonly ISender _sender;

    public EnrollmentController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    //[Authorise(Roles = "Student")]
    public async Task<IActionResult> Enroll([FromBody] EnrollCourseCommand command)
    {
        try
        {
            await _sender.Send(command);
            return Ok(new { EnrollmentId = command.CourseCode });
        }
        catch (Exception ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
    }
}