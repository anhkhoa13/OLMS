//using MediatR;
//using OLMS.Application.Features.QuizUC.DTO;
//using OLMS.Domain.Entities.QuizEntity;
//using OLMS.Domain.Repositories;
//using OLMS.Domain.Result;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace OLMS.Application.Features.QuizUC.Command;
//public record SubmitQuizCommand(Guid AttemptId, List<StudentAnswerDto> Answers) : IRequest<Result<double>>;

//public class SubmitQuizCommandHandler : IRequestHandler<SubmitQuizCommand, Result<double>> {
//    private readonly IQuizAttemptRepository _quizAttemptRepo;
//    private readonly IQuizRepository _quizRepo;
//    private readonly IStudentAnswerRepository _studentAnswerRepo;
//    private readonly IUnitOfWork _unitOfWork;

//    public SubmitQuizCommandHandler(
//        IQuizAttemptRepository quizAttemptRepo,
//        IStudentAnswerRepository studentAnswerRepo,
//        IQuizRepository quizRepo,
//        IUnitOfWork unitOfWork) {
//        _quizAttemptRepo = quizAttemptRepo;
//        _studentAnswerRepo = studentAnswerRepo;
//        _quizRepo = quizRepo;
//        _unitOfWork = unitOfWork;
//    }

//    public async Task<Result<double>> Handle(SubmitQuizCommand request, CancellationToken cancellationToken) {
//        var attempt = await _quizAttemptRepo.GetByIdAsync(request.AttemptId, cancellationToken);
//        if (attempt == null)
//            return Result<double>.Failure(Error.NotFound("Quiz Attempt not found"));


//        var quiz = await _quizRepo.GetByIdAsync(attempt.QuizId, cancellationToken);
//        if (quiz == null)
//            return Result<double>.Failure(Error.NotFound("Quiz Attempt not found"));


//        int correctAnswers = 0;

//        foreach (var answerDto in request.Answers) {
//            var question = quiz.Questions.FirstOrDefault(q => q.Id == answerDto.QuestionId);
//            if (question == null) continue;

//            var studentAnswer = new StudentResponse(
//                Guid.NewGuid(), request.AttemptId, answerDto.QuestionId, answerDto.Answer);

//            if (question.IsCorrect(answerDto.Answer)) correctAnswers++;

//            await _studentAnswerRepo.AddAsync(studentAnswer, cancellationToken);
//        }

//        attempt.Score = (double)correctAnswers / quiz.Questions.Count * 100;
//        attempt.SubmittedAt = DateTime.Now;
//        attempt.Status = QuizAttemptStatus.Submitted;

//        _quizAttemptRepo.Update(attempt);
//        await _unitOfWork.SaveChangesAsync(cancellationToken);

//        // ✅ Return the score using Result<T>
//        return Result<double>.Success(attempt.Score);
//    }

//}

