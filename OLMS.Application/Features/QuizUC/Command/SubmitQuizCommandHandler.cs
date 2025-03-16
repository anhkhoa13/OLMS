using MediatR;
using OLMS.Application.Features.QuizUC.DTO;
using OLMS.Domain.Entities.QuizEntity;
using OLMS.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OLMS.Application.Features.QuizUC.Command;
public record SubmitQuizCommand(Guid AttemptId, List<StudentAnswerDto> Answers) : IRequest<bool>;
public class SubmitQuizCommandHandler : IRequestHandler<SubmitQuizCommand, bool>
{
    private readonly IQuizAttemptRepository _quizAttemptRepo;
    private readonly IQuizRepository _quizRepo;
    private readonly IStudentAnswerRepository _studentAnswerRepo;
    private readonly IUnitOfWork _unitOfWork;
    public SubmitQuizCommandHandler(
        IQuizAttemptRepository quizAttemptRepo,
        IStudentAnswerRepository studentAnswerRepo,
        IQuizRepository quizRepo,
        IUnitOfWork unitOfWork)
    {
        _quizAttemptRepo = quizAttemptRepo;
        _studentAnswerRepo = studentAnswerRepo;
        _quizRepo = quizRepo;
        _unitOfWork = unitOfWork;
    }
    public async Task<bool> Handle(SubmitQuizCommand request, CancellationToken cancellationToken)
    {
        var attempt = await _quizAttemptRepo.GetByIdAsync(request.AttemptId, cancellationToken);
        if (attempt == null) throw new Exception("Quiz Attempt not found");

        var quiz = await _quizRepo.GetByIdAsync(attempt.QuizId, cancellationToken);
        if (quiz == null) throw new Exception("Quiz not found");

        int correctAnswers = 0;

        foreach (var answerDto in request.Answers)
        {
            var question = quiz.Questions.FirstOrDefault(q => q.Id == answerDto.QuestionId);
            if (question == null) continue;

            var studentAnswer = new StudentAnswer(
                Guid.NewGuid(), request.AttemptId, answerDto.QuestionId, answerDto.Answer);

            if (studentAnswer.IsCorrect()) correctAnswers++;

            await _studentAnswerRepo.AddAsync(studentAnswer, cancellationToken);
        }

        attempt.Score = (double)correctAnswers / quiz.Questions.Count * 100;
        attempt.Status = QuizAttemptStatus.Submitted;

        _quizAttemptRepo.Update(attempt);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return true;
    }
}
