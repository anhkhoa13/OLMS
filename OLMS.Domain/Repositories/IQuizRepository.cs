using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OLMS.Domain.Entities;
using OLMS.Domain.Entities.Quiz;

namespace OLMS.Domain.Repositories;

public interface IQuizRepository : IRepository<Quiz>
{
    public Task<MultipleChoiceQuiz?> GetMultipleChoiceQuizByIdAsync(Guid id, CancellationToken cancellationToken);
}
