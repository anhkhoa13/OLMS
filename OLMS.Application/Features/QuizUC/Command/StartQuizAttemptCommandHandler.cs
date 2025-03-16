using MediatR;
using OLMS.Application.Features.QuizUC.DTO;
using OLMS.Domain.Entities.QuizEntity;
using OLMS.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OLMS.Application.Features.QuizUC.Command;
public record StartQuizAttemptCommand(Guid StudentId, Guid QuizId) : IRequest<Guid>;
public class StartQuizAttemptCommandHandler : IRequestHandler<StartQuizAttemptCommand, Guid>
{
    private readonly IQuizRepository _quizRepo;
    private readonly IQuizAttemptRepository _attemptRepo;

    public StartQuizAttemptCommandHandler(IQuizRepository quizRepo, IQuizAttemptRepository attemptRepo)
    {
        _quizRepo = quizRepo;
        _attemptRepo = attemptRepo;
    }

    public async Task<Guid> Handle(StartQuizAttemptCommand request, CancellationToken cancellationToken)
    {
        // Fetch quiz
        var quiz = await _quizRepo.GetByIdAsync(request.QuizId);
        if (quiz == null) throw new Exception("Quiz not found.");

        // check student eligible
        var attempt = new QuizAttempt(Guid.NewGuid(), request.StudentId, request.QuizId, DateTime.UtcNow, QuizAttemptStatus.InProgress);
        await _attemptRepo.AddAsync(attempt, cancellationToken);
        await _attemptRepo.SaveChangesAsync();

        // 4. Return response
        return attempt.Id;
    }
}
