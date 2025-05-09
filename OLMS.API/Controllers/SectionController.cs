﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using OLMS.Application.Features.SectionUC;

[ApiController]
[Route("api/section")]
public class SectionController : ControllerBase {
    private readonly ISender _sender;

    public SectionController(ISender sender) {
        _sender = sender;
    }

    /// <summary>
    /// Gets all sections for a course, including lessons, section items, and assignments.
    /// </summary>
    [HttpGet("course")]
    public async Task<IActionResult> GetSectionList(Guid courseId) {
        var query = new GetSectionDetailsQuery(courseId);
        var result = await _sender.Send(query);

        if (!result.IsSuccess)
            return NotFound(result.Error);

        return Ok(result.Value);
    }

    [HttpGet]
    public async Task<IActionResult> GetSection(Guid sectionId) {
        var query = new GetSectionItemQuery(sectionId);
        var result = await _sender.Send(query);

        if (!result.IsSuccess)
            return NotFound(result.Error);

        return Ok(result.Value);
    }

    [HttpPut("update")]
    public async Task<IActionResult> UpdateSection([FromBody] UpdateSectionCommand command)
    {
        var result = await _sender.Send(command);
        if (!result.IsSuccess)
            return BadRequest(result.Error);
        return Ok(new { Message = "Update section successfully"});
    }

    [HttpPut("order")]
    public async Task<IActionResult> UpdateSectionOrder([FromBody] UpdateSectionOrderCommand command) {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var result = await _sender.Send(command);
        if (!result.IsSuccess)
            return BadRequest(result.Error);

        return Ok(new { Message = "Section order updated successfully" });
    }

    [HttpPut("item/order")]
    public async Task<IActionResult> UpdateSectionItemOrder([FromBody] UpdateSectionItemOrderCommand command) {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var result = await _sender.Send(command);
        return result.IsSuccess ? Ok() : BadRequest(result.Error);
    }
    [HttpDelete("delete/{sectionId}")]
    public async Task<IActionResult> DeleteSection(Guid sectionId) {
        var command = new DeleteSectionCommand(sectionId);
        var result = await _sender.Send(command);

        return result.IsSuccess ? NoContent() : BadRequest(result.Error);
    }
}
