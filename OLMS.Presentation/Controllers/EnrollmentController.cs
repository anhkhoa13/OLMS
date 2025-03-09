using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OLMS.Application.Feature.Enrollment;

namespace OLMS.API.Controllers
{
    [Route("api/enrollments")]
    [ApiController]
    public class EnrollmentController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EnrollmentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Authorize(Roles = "Student")]
        public async Task<IActionResult> Enroll([FromBody] EnrollCourseCommand command)
        {
            try
            {
                var enrollmentId = await _mediator.Send(command);
                return Ok(new { EnrollmentId = enrollmentId });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
    }
}