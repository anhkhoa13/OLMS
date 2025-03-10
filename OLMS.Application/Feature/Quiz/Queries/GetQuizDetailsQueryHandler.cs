using MediatR;
using OLMS.Domain.Entities.Quiz;
using OLMS.Domain.Repositories;

public class GetQuizDetailsQueryHandler : IRequestHandler<GetQuizDetailsQuery, Quiz>
{
    private readonly IQuizRepository _quizRepo;

    public GetQuizDetailsQueryHandler(IQuizRepository quizRepo)
    {
        _quizRepo = quizRepo;
    }

    public async Task<Quiz> Handle(GetQuizDetailsQuery request, CancellationToken cancellationToken)
    {
        return await _quizRepo.GetByIdAsync(request.QuizId) ?? throw new Exception("Quiz not found.");
    }
}

