

using OLMS.Domain.Entities.ForumAggregate.PostAggregate;
using OLMS.Domain.Repositories;

namespace OLMS.Infrastructure.Database.Repositories;

public class VoteRepository : Repository<Vote>, IVoteRepository
{
    public VoteRepository(ApplicationDbContext context) : base(context)
    {
    }
    
}

