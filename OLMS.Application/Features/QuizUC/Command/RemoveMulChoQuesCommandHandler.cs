using MediatR;
using OLMS.Application.Feature.CourseUC;
using OLMS.Domain.Entities;
using OLMS.Domain.Entities.QuizEntity;
using OLMS.Domain.Repositories;

namespace OLMS.Application.Features.QuizUC.Command;
public record RemoveMulChQuesCommand(Guid QuizId, Guid QuestionId) : IRequest<bool>
{
}
public class RemoveMulChoQuesCommandHandler : IRequestHandler<RemoveMulChQuesCommand, bool>
{
    private readonly IQuizRepository _quizRepo;
    private readonly IUnitOfWork _unitOfWork;
    public RemoveMulChoQuesCommandHandler(IQuizRepository quizRepo, IUnitOfWork unitOfWork)
    {
        _quizRepo = quizRepo;
        _unitOfWork = unitOfWork;
    }
    public async Task<bool> Handle(RemoveMulChQuesCommand request, CancellationToken cancellationToken)
    {
        var quiz = await _quizRepo.GetByIdAsync(request.QuizId, cancellationToken);

        var question = quiz.Questions.FirstOrDefault(q => q.Id == request.QuestionId);
        if (question == null) return false;
        question.IsDeleted = true;  // Instead of removing, mark as deleted
        //quiz.RemoveQuestion(question); // No hard delete
        _quizRepo.Update(quiz);
        await _unitOfWork.SaveChangesAsync();

        return true;
    }

}
