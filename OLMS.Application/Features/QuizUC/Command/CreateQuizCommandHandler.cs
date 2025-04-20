using MediatR;
using OLMS.Domain.Entities.QuizEntity;
using OLMS.Domain.Repositories;

namespace OLMS.Application.Features.QuizUC.Command;
public record CreateQuizCommand(
    Guid InstructorId,
    string Title,
    string Description,
    DateTime StartTime,
    DateTime EndTime,
    bool IsTimeLimited,
    TimeSpan? TimeLimit,
    int NumberOfAttempts
    ) : IRequest<Guid> {
}
public class CreateQuizCommandHandler : IRequestHandler<CreateQuizCommand, Guid>
{
    private readonly IQuizRepository _quizRepo;
    private readonly IUnitOfWork _unitOfWork;

    public CreateQuizCommandHandler(IQuizRepository repository, IUnitOfWork unitOfWork)
    {
        _quizRepo = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> Handle(CreateQuizCommand request, CancellationToken cancellationToken)
    {
        var quiz = Quiz.Create(
            Guid.NewGuid(),
            request.InstructorId,
            request.Title,
            request.Description,
            request.StartTime,
            request.EndTime,
            request.IsTimeLimited,
            request.TimeLimit,
            request.NumberOfAttempts
        );
        await _quizRepo.AddAsync(quiz);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return quiz.Id;
    }
}
