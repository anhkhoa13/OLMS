//using MediatR;
//using OLMS.Application.Feature.CourseUC;
//using OLMS.Domain.Entities;
//using OLMS.Domain.Entities.QuizEntity;
//using OLMS.Domain.Repositories;
//using OLMS.Domain.Result;

//namespace OLMS.Application.Features.QuizUC.Command;
//public record RemoveQuestionCommand(Guid QuizId, Guid QuestionId) : IRequest<Result>
//{
//}
//public class RemoveQuestionCommandHandler : IRequestHandler<RemoveQuestionCommand, Result>
//{
//    private readonly IQuizRepository _quizRepo;
//    private readonly IUnitOfWork _unitOfWork;
//    public RemoveQuestionCommandHandler(IQuizRepository quizRepo, IUnitOfWork unitOfWork)
//    {
//        _quizRepo = quizRepo;
//        _unitOfWork = unitOfWork;
//    }
//    public async Task<Result> Handle(RemoveQuestionCommand request, CancellationToken cancellationToken) {
//        var quiz = await _quizRepo.GetByIdAsync(request.QuizId, cancellationToken);
//        if (quiz == null) {
//            return Result.Failure(new Error("Quiz not found"));
//        }

//        var question = quiz.Questions.FirstOrDefault(q => q.Id == request.QuestionId);
//        if (question == null) {
//            return Result.Failure(new Error("Question not found"));
//        }

//        // Mark question as deleted
//        question.IsDeleted = true;
//        _quizRepo.Update(quiz);
//        await _unitOfWork.SaveChangesAsync();

//        return Result.Success();
//    }

//}
