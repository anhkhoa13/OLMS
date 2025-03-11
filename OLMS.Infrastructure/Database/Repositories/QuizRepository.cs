using Microsoft.EntityFrameworkCore;
using OLMS.Domain.Entities;
using OLMS.Domain.Entities.Quiz;
using OLMS.Domain.Repositories;

namespace OLMS.Infrastructure.Database.Repositories;

public class QuizRepository : Repository<Quiz>, IQuizRepository
{
    public QuizRepository(ApplicationDbContext context) : base(context) {}

    public async Task<MultipleChoiceQuiz> GetMultipleChoiceQuizByIdAsync(Guid quizId, CancellationToken cancellationToken)
    {
        return await _context.MultipleChoiceQuizzes
            .Include(q => q.QuestionsCollection)
            .FirstOrDefaultAsync(q => q.Id == quizId);
    }

}