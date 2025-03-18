using Microsoft.EntityFrameworkCore;
using OLMS.Domain.Entities;
using OLMS.Domain.Entities.QuizEntity;
using OLMS.Domain.Repositories;
using OLMS.Domain.ValueObjects;

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
    public async Task<Quiz?> GetByCodeAsync(string code)
    {
        return await _context.Quizzes
            .Include(q => q.Questions.Where(q => !q.IsDeleted))
            .SingleOrDefaultAsync(q => q.Code.Value == code);
    }
}