using MediatR;
using OLMS.Application.Feature.CourseUC;
using OLMS.Domain.Entities;
using OLMS.Domain.Entities.QuizEntity;
using OLMS.Domain.Repositories;

namespace OLMS.Application.Feature.QuizUC.Command;
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
        var quiz = await _quizRepo.GetByIdAsync(request.QuizId, cancellationToken);

        var question = quiz.Questions.FirstOrDefault(q => q.Id == request.QuestionId);
        if (question == null) return false;

        quiz.RemoveQuestion(question);
        _quizRepo.Update(quiz);
        await _quizRepo.SaveChangesAsync(cancellationToken);

        return true;
    }

}
