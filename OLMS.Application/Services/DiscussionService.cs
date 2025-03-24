using OLMS.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLMS.Application.Services
{
    public interface IDiscussionService
    {
        Task<Discussion> CreateDiscussionAsync(Discussion discussion);
        Task<IEnumerable<Discussion>> GetDiscussionsByCourseIdAsync(string courseId);
    }

    public class DiscussionService : IDiscussionService
    {
        private static readonly List<Discussion> _discussions = new();

        public async Task<Discussion> CreateDiscussionAsync(Discussion discussion)
        {
            _discussions.Add(discussion);
            return await Task.FromResult(discussion);
        }

        public async Task<IEnumerable<Discussion>> GetDiscussionsByCourseIdAsync(string courseId)
        {
            var discussions = _discussions.Where(d => d.CourseId == courseId);
            return await Task.FromResult(discussions);
        }
    }
}
