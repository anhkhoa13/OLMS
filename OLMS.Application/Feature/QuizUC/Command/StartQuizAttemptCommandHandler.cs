using MediatR;
using OLMS.Application.Feature.QuizUC.Command.DTO;
using OLMS.Domain.Entities.QuizEntity;
using OLMS.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OLMS.Application.Feature.QuizUC.Command;
public record StartQuizAttemptCommand(Guid StudentId, Guid QuizId) : IRequest<StartQuizResponse>;
public class StartQuizResponse
{
    public Guid QuizAttemptId { get; set; }
    public string QuizTitle { get; set; }
    public List<QuestionDto> Questions { get; set; }
}

public class StartQuizAttemptCommandHandler : IRequestHandler<StartQuizAttemptCommand, StartQuizResponse>
{
    private readonly IQuizRepository _quizRepo;
    private readonly IQuizAttemptRepository _attemptRepo;

    public StartQuizAttemptCommandHandler(IQuizRepository quizRepo, IQuizAttemptRepository attemptRepo)
    {
        _quizRepo = quizRepo;
        _attemptRepo = attemptRepo;
    }

    public async Task<StartQuizResponse> Handle(StartQuizAttemptCommand request, CancellationToken cancellationToken)
    {
        // Fetch quiz
        var quiz = await _quizRepo.GetByIdAsync(request.QuizId);
        if (quiz == null) throw new Exception("Quiz not found.");

        // check student eligible

        var attempt = new QuizAttempt(Guid.NewGuid(), request.StudentId, request.QuizId, DateTime.UtcNow, QuizAttemptStatus.InProgress);
        await _attemptRepo.AddAsync(attempt, cancellationToken);
        await _attemptRepo.SaveChangesAsync();

        // 4. Return response
        return new StartQuizResponse
        {
            QuizAttemptId = attempt.Id,
            QuizTitle = quiz.Title,
            Questions = quiz.Questions.Select(q =>
            {
                var questionDto = new QuestionDto
                {
                    QuestionId = q.Id,
                    Content = q.Content,
                    Type = q.Type
                };

                if (q is MultipleChoiceQuestion mcq)
                    questionDto.Options = mcq.Options;

                return questionDto;
            }).ToList()
        };
    }
}
