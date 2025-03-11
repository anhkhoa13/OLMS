using MediatR;
using OLMS.Application.Feature.CourseUC;
using OLMS.Domain.Entities;
using OLMS.Domain.Entities.Quiz;
using OLMS.Domain.Repositories;

namespace OLMS.Application.Feature.Quiz.Command;
public record RemoveMulChQuesCommand(Guid QuizId, Guid QuestionId) : IRequest<bool>
{
}
public class RemoveMulChoQuesCommandHandler : IRequestHandler<RemoveMulChQuesCommand, bool>
{
    private readonly IQuizRepository _quizRepo;
    public RemoveMulChoQuesCommandHandler(IQuizRepository quizRepo)
    {
        _quizRepo = quizRepo;
    }
    public async Task<bool> Handle(RemoveMulChQuesCommand request, CancellationToken cancellationToken)
    {
        var quiz = await _quizRepo.GetMultipleChoiceQuizByIdAsync(request.QuizId, cancellationToken);
        if (quiz is not MultipleChoiceQuiz multipleChoiceQuiz) return false;

        var question = multipleChoiceQuiz.Questions.FirstOrDefault(q => q.Id == request.QuestionId);
        if (question == null) return false;

        multipleChoiceQuiz.RemoveQuestion(question);
        _quizRepo.Update(multipleChoiceQuiz);
        await _quizRepo.SaveChangesAsync(cancellationToken);

        return true;
    }
}
