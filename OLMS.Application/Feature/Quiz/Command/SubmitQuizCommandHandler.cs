using MediatR;
using OLMS.Domain.Entities.Quiz;
using OLMS.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OLMS.Application.Feature.Quiz.Command;

public class SubmitQuizCommandHandler : IRequestHandler<SubmitQuizCommand, bool>
{
    private readonly IQuizAttemptRepository _quizAttemptRepo;
    private readonly IStudentAnswerRepository _studentAnswerRepo;
    public SubmitQuizCommandHandler(
        IQuizAttemptRepository quizAttemptRepo,
        IStudentAnswerRepository studentAnswerRepo)
    {
        _quizAttemptRepo = quizAttemptRepo;
        _studentAnswerRepo = studentAnswerRepo;
    }
    public async Task<bool> Handle(SubmitQuizCommand request, CancellationToken cancellationToken)
    {
        // Retrieve existing quiz attempt
        var attempt = await _quizAttemptRepo.GetByIdAsync(request.AttemptId);
        if (attempt == null || attempt.IsSubmitted)
            return false;

        // Save student answers
        var answers = request.Answers.Select(a =>
            new StudentAnswer(Guid.NewGuid(), attempt.Id, a.QuestionId, a.Answer)).ToList();

        await _studentAnswerRepo.AddRangeAsync(answers, cancellationToken);
        await _studentAnswerRepo.SaveChangesAsync(cancellationToken);

        // Mark attempt as submitted
        attempt.Submit();
        _quizAttemptRepo.Update(attempt);
        await _quizAttemptRepo.SaveChangesAsync(cancellationToken);
        return true;
    }
}
