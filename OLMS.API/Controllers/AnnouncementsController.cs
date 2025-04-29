using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace OLMS.API.Controllers {
    [ApiController]
    [Route("api/announcement")]
    public class AnnouncementsController : ControllerBase {
        private readonly IMediator _mediator;

        public AnnouncementsController(IMediator mediator) {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAnnouncement([FromBody] CreateAnnouncementCommand command) {
            var result = await _mediator.Send(command);

            if (!result.IsSuccess)
                return BadRequest(result.Error);

            return CreatedAtAction(nameof(GetAnnouncementsByCourse),
                new { courseId = command.CourseId },
                new { Id = result.Value, Message = "Announcement created successfully" });
        }

        [HttpGet("course/{courseId}")]
        public async Task<IActionResult> GetAnnouncementsByCourse(Guid courseId) {
            var query = new GetAnnouncementsByCourseQuery(courseId);
            var result = await _mediator.Send(query);

            if (!result.IsSuccess)
                return BadRequest(result.Error);

            return Ok(result.Value);
        }
    }
}
