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
        try {
            var quiz = await _context.Quizzes
                .Include(q => q.Questions)
                .SingleOrDefaultAsync(q => q.Id == id, cancellationToken);

            if (quiz == null) {
                throw new Exception("Quiz not found.");
            }

            return quiz;
        } catch (Exception ex) {
            // Log the exception to get more information about the failure
            Console.WriteLine(ex.ToString());
            throw new Exception("An error occurred while fetching the quiz.");
        }
    }
    public async Task<Quiz?> GetByCodeAsync(string code)
    {
        return await _context.Quizzes
            .Include(q => q.Questions.Where(q => !q.IsDeleted))
            .SingleOrDefaultAsync(q => q.Code.Value == code);
    }
    public virtual async Task<IEnumerable<Quiz>> GetAllQuizsAsyncIncludeQuestions(CancellationToken cancellationToken)
    {
        return await _context.Quizzes
            .Include(q => q.Questions.Where(q => !q.IsDeleted))
            .ToListAsync(cancellationToken);
    }
}