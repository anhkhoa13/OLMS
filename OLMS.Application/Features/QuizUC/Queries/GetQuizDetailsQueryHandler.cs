using Mapster;
using MediatR;
using OLMS.Application.Features.QuizUC.DTO;
using OLMS.Domain.Entities.QuestionEntity;
using OLMS.Domain.Repositories;
using OLMS.Domain.ValueObjects;
public record GetQuizDetailsQuery : IRequest<QuizDto>
{
    public string Code { get; set; }
}

public class GetQuizDetailsQueryHandler : IRequestHandler<GetQuizDetailsQuery, QuizDto>
{
    private readonly IQuizRepository _quizRepo;

    public GetQuizDetailsQueryHandler(IQuizRepository quizRepo)
    {
        _quizRepo = quizRepo;
    }

    public async Task<QuizDto> Handle(GetQuizDetailsQuery request, CancellationToken cancellationToken)
    {
        var quiz = await _quizRepo.GetByCodeAsync(request.Code);
        if (quiz == null) throw new Exception("Quiz not found");

        return new QuizDto
        {
            QuizId = quiz.Id,
            Title = quiz.Title,
            Description = quiz.Description,
            StartTime = quiz.StartTime,
            EndTime = quiz.EndTime,
            IsTimeLimited = quiz.IsTimeLimited,
            Questions = quiz.Questions.Select(q => q.Adapt<QuestionDto>()).ToList()
        };
    }
}

