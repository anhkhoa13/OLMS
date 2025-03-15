using Microsoft.EntityFrameworkCore;
using OLMS.Domain.Entities;
using OLMS.Domain.Entities.QuizEntity;
using OLMS.Domain.Repositories;

namespace OLMS.Infrastructure.Database.Repositories;

public class QuizRepository : Repository<Quiz>, IQuizRepository
{
    public QuizRepository(ApplicationDbContext context) : base(context) {}

    public override async Task<Quiz?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _context.Quizzes
            .Include(q => q.Questions)
            .SingleOrDefaultAsync(q => q.Id == id, cancellationToken);
    }
}