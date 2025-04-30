

using OLMS.Domain.Entities.AssignmentAttempt;
using OLMS.Domain.Repositories;

namespace OLMS.Infrastructure.Database.Repositories;

public class SubmitAttachmentRepository : Repository<SubmitAttachment>, ISubmitAttachmentRepository
{
    public SubmitAttachmentRepository(ApplicationDbContext context) : base(context)
    {
    }
}
