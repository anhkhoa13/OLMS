using Microsoft.EntityFrameworkCore;
using OLMS.Domain.Entities;
using OLMS.Domain.Entities.Quiz;
using OLMS.Domain.Repositories;

namespace OLMS.Infrastructure.Database.Repositories;

public class QuizRepository : Repository<Quiz>, IQuizRepository
{
    public QuizRepository(ApplicationDbContext context) : base(context) {}
}