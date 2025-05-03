using Mapster;
using MediatR;
using OLMS.Application.Features.QuizUC.DTO;
using OLMS.Domain.Entities.QuestionEntity;
using OLMS.Domain.Repositories;
using OLMS.Domain.ValueObjects;
public record GetQuizDetailsQuery : IRequest<QuizDto> {
    public string Code { get; set; }
}

public class GetQuizDetailsQueryHandler : IRequestHandler<GetQuizDetailsQuery, QuizDto> {
    private readonly IQuizRepository _quizRepo;

    public GetQuizDetailsQueryHandler(IQuizRepository quizRepo) {
        _quizRepo = quizRepo;
    }

    public async Task<QuizDto> Handle(GetQuizDetailsQuery request, CancellationToken cancellationToken) {
        var quiz = await _quizRepo.GetByCodeAsync(request.Code);
        if (quiz == null) throw new Exception("Quiz not found");

        var questionDtos = quiz.Questions.Select(q =>
        {
            var dto = new QuestionDto {
                QuestionId = q.Id,
                Content = q.Content,
                Type = q.GetType().Name switch {
                    nameof(MultipleChoiceQuestion) => "MultipleChoice",
                    nameof(ShortAnswerQuestion) => "ShortAnswer",
                    _ => "Unknown"
                }
            };

            if (q is MultipleChoiceQuestion mcq) {
                dto.Options = mcq.Options;
                dto.CorrectOptionIndex = mcq.CorrectOptionIndex;
            }

            if (q is ShortAnswerQuestion saq) {
                dto.CorrectAnswer = saq.CorrectAnswer;
            }

            return dto;
        }).ToList();

        return new QuizDto {
            QuizId = quiz.Id,
            Code = quiz.Code,
            Title = quiz.Title,
            Description = quiz.Description,
            StartTime = quiz.StartDate,
            EndTime = quiz.DueDate,
            IsTimeLimited = quiz.IsTimeLimited,
            TimeLimit = quiz.TimeLimit,
            NumberOfAttempts = quiz.NumberOfAttempts,
            Questions = questionDtos
        };
    }

}

