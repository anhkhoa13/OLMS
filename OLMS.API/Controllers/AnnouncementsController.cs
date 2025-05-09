using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace OLMS.API.Controllers {
    [ApiController]
    [Route("api/announcement")]
    public class AnnouncementsController : ControllerBase {
        private readonly ISender _sender;

        public AnnouncementsController(ISender sender) {
            _sender = sender;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAnnouncement([FromBody] CreateAnnouncementCommand command) {
            var result = await _sender.Send(command);

            if (!result.IsSuccess)
                return BadRequest(result.Error);

            return CreatedAtAction(nameof(GetAnnouncementsByCourse),
                new { courseId = command.CourseId },
                new { Id = result.Value, Message = "Announcement created successfully" });
        }

        [HttpGet("course/{courseId}")]
        public async Task<IActionResult> GetAnnouncementsByCourse(Guid courseId) {
            var query = new GetAnnouncementsByCourseQuery(courseId);
            var result = await _sender.Send(query);

            if (!result.IsSuccess)
                return BadRequest(result.Error);

            return Ok(result.Value);
        }
        [HttpDelete("delete/{announcementId}")]
        public async Task<IActionResult> DeleteAnnouncement(Guid announcementId) {
            var command = new DeleteAnnouncementCommand(announcementId);
            var result = await _sender.Send(command);

            return result.IsSuccess ? NoContent() : BadRequest(result.Error);
        }

    }
}
