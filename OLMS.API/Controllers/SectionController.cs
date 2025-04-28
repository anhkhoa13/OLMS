using MediatR;
using Microsoft.AspNetCore.Mvc;

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
}
