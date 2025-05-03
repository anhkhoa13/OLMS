using MediatR;
using Microsoft.EntityFrameworkCore;
using OLMS.Domain.Entities.QuestionEntity;
using OLMS.Domain.Repositories;
using OLMS.Domain.Result;

public record UpdateQuestionsCommand(
    Guid QuizId,
    List<QuestionUpdateDto> Questions
) : IRequest<Result<List<Guid>>>;

public record QuestionUpdateDto(
    Guid QuestionId,
    string Content,
    string Type,  // "MultipleChoice" or "ShortAnswer"
    List<string>? Options,
    int? CorrectOptionIndex,
    string? CorrectAnswer
);

// Handler
public class UpdateQuestionsCommandHandler : IRequestHandler<UpdateQuestionsCommand, Result<List<Guid>>> {
    private readonly IQuizRepository _quizRepo;
    private readonly IQuestionRepository _quesRepo;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateQuestionsCommandHandler(
        IQuestionRepository quesRepo,
        IQuizRepository quizRepo,
        IUnitOfWork unitOfWork) {
        _quesRepo = quesRepo;
        _quizRepo = quizRepo;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<List<Guid>>> Handle(UpdateQuestionsCommand request, CancellationToken cancellationToken) {
        try {
            var quiz = await _quizRepo.GetByIdAsync(request.QuizId);
            if (quiz == null) return new Error("Quiz not found");

            var updatedIds = new List<Guid>();
            foreach (var dto in request.Questions) {
                var question = await _quesRepo.GetByIdAsync(dto.QuestionId);
                if (question == null) continue;  // or return error

                // Validate question type consistency
                if (!Enum.TryParse<QuestionType>(dto.Type, out var parsedType) ||
    question.Type != parsedType) {
                    return new Error($"Question {dto.QuestionId} type mismatch");
                }

                UpdateQuestionFromDto(question, dto);
                _quesRepo.Update(question);
                updatedIds.Add(question.Id);
            }

            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Result<List<Guid>>.Success(updatedIds);
        } catch (DbUpdateException dbEx) {
            var innerMessage = dbEx.InnerException?.Message ?? dbEx.Message;
            return new Error($"Database error updating questions: {innerMessage}");
        } catch (Exception ex) {
            return new Error($"Error updating questions: {ex.Message}");
        }
    }

    private void UpdateQuestionFromDto(Question question, QuestionUpdateDto dto) {
        question.UpdateContent(dto.Content);

        switch (question) {
            case MultipleChoiceQuestion mcq:
                mcq.Options = dto.Options ?? throw new ArgumentNullException(nameof(dto.Options));
                mcq.CorrectOptionIndex = dto.CorrectOptionIndex
                    ?? throw new ArgumentNullException(nameof(dto.CorrectOptionIndex));
                break;

            case ShortAnswerQuestion saq:
                saq.CorrectAnswer = dto.CorrectAnswer
                    ?? throw new ArgumentNullException(nameof(dto.CorrectAnswer));
                break;
        }
    }
}
