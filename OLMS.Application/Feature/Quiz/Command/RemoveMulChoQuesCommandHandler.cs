using MediatR;
using OLMS.Application.Feature.CourseUC;
using OLMS.Domain.Entities;
using OLMS.Domain.Entities.Quiz;
using OLMS.Domain.Repositories;

namespace OLMS.Application.Feature.Quiz.Command;

public class RemoveMulChoQuesCommandHandler : IRequestHandler<RemoveMulChoiceQuizCommand, bool>
{
    private readonly IQuizRepository _quizRepo;
    public RemoveMulChoQuesCommandHandler(IQuizRepository quizRepo)
    {
        _quizRepo = quizRepo;
    }
    public async Task<bool> Handle(RemoveMulChoiceQuizCommand request, CancellationToken cancellationToken)
    {
        var quiz = await _quizRepo.GetByIdAsync(request.QuizId, cancellationToken);
        if (quiz is not MultipleChoiceQuiz multipleChoiceQuiz) return false;

        var question = multipleChoiceQuiz.Questions.FirstOrDefault(q => q.Id == request.QuestionId);
        if (question == null) return false;

        multipleChoiceQuiz.RemoveQuestion(question);
        _quizRepo.Update(quiz);
        await _quizRepo.SaveChangesAsync(cancellationToken);

        return true;
    }
}
