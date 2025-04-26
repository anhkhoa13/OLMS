//using Microsoft.AspNetCore.Mvc;
//using OLMS.Application.Services;
//using OLMS.Domain.Entities;
//using System.Collections.Generic;
//using System.Threading.Tasks;

//namespace OLMS.API.Controllers
//{
//    [Route("api/discussions")]
//    [ApiController]
//    public class DiscussionController : ControllerBase
//    {
//        private readonly IDiscussionService _discussionService;

//        public DiscussionController(IDiscussionService discussionService)
//        {
//            _discussionService = discussionService;
//        }

//        [HttpPost]
//        public async Task<IActionResult> CreateDiscussion([FromBody] Discussion discussion)
//        {
//            if (string.IsNullOrWhiteSpace(discussion.ForumId) || string.IsNullOrWhiteSpace(discussion.CreatorId) || string.IsNullOrWhiteSpace(discussion.Content))
//            {
//                return BadRequest("ForumId, CreatorId, and Content are required.");
//            }

//            var discussions = await _discussionService.GetDiscussionsByCourseIdAsync(discussion.ForumId);
//            int count = discussions.Count() + 1;
//            discussion.Id = $"{discussion.ForumId}-D{count}";

//            var createdDiscussion = await _discussionService.CreateDiscussionAsync(discussion);
//            return CreatedAtAction(nameof(GetDiscussionsByCourse), new { courseId = discussion.ForumId }, createdDiscussion);
//        }

//        [HttpGet("{courseId}")]
//        public async Task<IActionResult> GetDiscussionsByCourse(string courseId)
//        {
//            var discussions = await _discussionService.GetDiscussionsByCourseIdAsync(courseId);
//            return Ok(discussions);
//        }
//    }
//}
