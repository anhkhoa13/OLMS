using MediatR;
using OLMS.Domain.Entities.QuizEntity;
using OLMS.Domain.Repositories;

namespace OLMS.Application.Features.QuizUC.Command;
public record CreateQuizCommand(
    string Title,
    string Description,
    DateTime StartTime,
    DateTime EndTime,
    bool IsTimeLimited
    ) : IRequest<Guid>
{
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
        Quiz quiz = Quiz.Create(
            Guid.NewGuid(), 
            request.Title, 
            request.Description, 
            request.StartTime, 
            request.EndTime, 
            request.IsTimeLimited);
        /*foreach (var questionCommand in request.Questions)
        {
            var question = new MultipleChoiceQuestion(Guid.NewGuid(),questionCommand.Content, questionCommand.Options, questionCommand.CorrectOptionIndex);
            quiz.AddQuestion(question);
        }*/
        await _quizRepo.AddAsync(quiz);
        //await _quizRepo.SaveChangesAsync();
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return quiz.Id;
    }
}
