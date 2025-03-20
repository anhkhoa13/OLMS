using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OLMS.Domain.Entities;
using OLMS.Domain.Entities.QuizEntity;
using OLMS.Domain.ValueObjects;

namespace OLMS.Domain.Repositories;

public interface IQuizRepository : IRepository<Quiz>
{
    public Task<Quiz?> GetByCodeAsync(string code);
    public Task<IEnumerable<Quiz>> GetAllQuizsAsyncIncludeQuestions(CancellationToken cancellationToken);
}
