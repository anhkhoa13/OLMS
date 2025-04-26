//using MediatR;
//using OLMS.Domain.Entities.QuizEntity;
//using OLMS.Domain.Repositories;
//using OLMS.Domain.Result;

//namespace OLMS.Application.Features.QuizUC.Command;
//public record StartQuizAttemptCommand(Guid StudentId, Guid QuizId) : IRequest<Result<Guid>>;
//public class StartQuizAttemptCommandHandler : IRequestHandler<StartQuizAttemptCommand, Result<Guid>>
//{
//    private readonly IQuizRepository _quizRepo;
//    private readonly IQuizAttemptRepository _attemptRepo;
//    private readonly IUnitOfWork _unitOfWork;

//    public StartQuizAttemptCommandHandler(IQuizRepository quizRepo, IQuizAttemptRepository attemptRepo, IUnitOfWork unitOfWork)
//    {
//        _quizRepo = quizRepo;
//        _attemptRepo = attemptRepo;
//        _unitOfWork = unitOfWork;
//    }

//    public async Task<Result<Guid>> Handle(StartQuizAttemptCommand request, CancellationToken cancellationToken)
//    {
//        // Fetch quiz
//        var quiz = await _quizRepo.GetByIdAsync(request.QuizId);
//        if (quiz == null) return Error.NotFound("Quiz not found.");

//        // Get previous attempts by the student for this quiz
//        var previousAttempts = await _attemptRepo.GetAttemptsByStudentAndQuizAsync(request.StudentId, request.QuizId);
//        var currentAttemptCount = previousAttempts.Count;

//        if (!quiz.CanStudentAttempt(currentAttemptCount))
//            return Error.Validation("You have reached the maximum number of allowed attempts for this quiz.");

//        // check student eligible
//        var attempt = new QuizAttempt(Guid.NewGuid(), request.StudentId, request.QuizId, DateTime.UtcNow, QuizAttemptStatus.InProgress) {
//            AttemptNumber = currentAttemptCount + 1
//        };
//        await _attemptRepo.AddAsync(attempt, cancellationToken);
//        await _unitOfWork.SaveChangesAsync();

//        // 4. Return response
//        return Result<Guid>.Success(attempt.Id);
//    }
//}
