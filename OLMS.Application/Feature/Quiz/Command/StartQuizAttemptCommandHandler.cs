using MediatR;
using OLMS.Domain.Entities.Quiz;
using OLMS.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OLMS.Application.Feature.Quiz.Command;

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
        var quiz = await _quizRepo.GetByIdAsync(request.QuizId);
        if (quiz == null) throw new Exception("Quiz not found.");

        var attempt = new QuizAttempt(Guid.NewGuid(), request.StudentId, request.QuizId);
        await _attemptRepo.AddAsync(attempt);
        await _attemptRepo.SaveChangesAsync();

        return attempt.Id;
    }
}
