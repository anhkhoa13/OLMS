using MediatR;
using OLMS.Application.Features.QuizUC.DTO;
using OLMS.Domain.Entities.QuestionEntity;
using OLMS.Domain.Repositories;

namespace OLMS.Application.Features.QuizUC.Command;
public record AddQuestionsCommand(Guid QuizId,
                                List<QuestionCreateDto> Questions) : IRequest<List<Guid>>
{
}
public class AddQuestionsCommandHandler : IRequestHandler<AddQuestionsCommand, List<Guid>>
{
    private readonly IQuizRepository _quizRepo;
    private readonly IQuestionRepository _quesRepo;
    private readonly IUnitOfWork _unitOfWork;
    public AddQuestionsCommandHandler(IQuestionRepository quesRepo, IQuizRepository quizRepo, IUnitOfWork unitOfWork)
    {
        _quizRepo = quizRepo;
        _quesRepo = quesRepo;
        _unitOfWork = unitOfWork;
    }
    public async Task<List<Guid>> Handle(AddQuestionsCommand request, CancellationToken cancellationToken)
    {
        var quiz = await _quizRepo.GetByIdAsync(request.QuizId);

        List<Guid> questionIds = new();
        foreach (var dto in request.Questions)
        {
            var question = QuestionFactory.CreateQuestion(Guid.NewGuid(), dto, request.QuizId);
            quiz.AddQuestion(question);
            await _quesRepo.AddAsync(question, cancellationToken);
            questionIds.Add(question.Id);
        }

        _quizRepo.Update(quiz);
        await _unitOfWork.SaveChangesAsync();

        return questionIds;
    }
}
