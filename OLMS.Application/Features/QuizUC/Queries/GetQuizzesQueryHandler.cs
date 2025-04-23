using Mapster;
using MediatR;
using OLMS.Application.Features.QuizUC.DTO;
using OLMS.Domain.Entities.QuestionEntity;
using OLMS.Domain.Repositories;
using OLMS.Domain.ValueObjects;

// only for debug
public record GetQuizzesQuery : IRequest<List<QuizDto>>
{

}

public class GetQuizzesQueryHandler : IRequestHandler<GetQuizzesQuery, List<QuizDto>>
{
    private readonly IQuizRepository _quizRepo;

    public GetQuizzesQueryHandler(IQuizRepository quizRepo)
    {
        _quizRepo = quizRepo;
    }

    public async Task<List<QuizDto>> Handle(GetQuizzesQuery request, CancellationToken cancellationToken)
    {
        var quizzes = await _quizRepo.GetAllQuizsAsyncIncludeQuestions(cancellationToken);
        if (quizzes == null || !quizzes.Any()) throw new Exception("Quiz not found");

        return quizzes.Adapt<List<QuizDto>>();
    }
}

