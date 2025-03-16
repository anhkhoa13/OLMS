using MediatR;
using OLMS.Domain.Entities.QuizEntity;
using OLMS.Domain.Repositories;

namespace OLMS.Application.Features.QuizUC.Command;
public record AddMulChoQuesCommand(Guid QuizId,
                                string Content,
                                List<string> Options,
                                int CorrectOptionIndex) : IRequest<Guid>
{
}
public class AddMulChoQuesCommandHandler : IRequestHandler<AddMulChoQuesCommand, Guid>
{
    private readonly IQuizRepository _quizRepo;
    private readonly IQuestionRepository _quesRepo;
    public AddMulChoQuesCommandHandler(IQuestionRepository quesRepo, IQuizRepository quizRepo)
    {
        _quizRepo = quizRepo;
        _quesRepo = quesRepo;
    }
    public async Task<Guid> Handle(AddMulChoQuesCommand request, CancellationToken cancellationToken)
    {
        var quiz = await _quizRepo.GetByIdAsync(request.QuizId);

        var question = new MultipleChoiceQuestion(Guid.NewGuid(), request.Content, request.Options, request.CorrectOptionIndex, request.QuizId);
        quiz.AddQuestion(question);

        await _quesRepo.AddAsync(question, cancellationToken);
        _quizRepo.Update(quiz);
        await _quesRepo.SaveChangesAsync();

        return question.Id;
    }
}
