using MediatR;
using Microsoft.EntityFrameworkCore;
using OLMS.Domain.Entities.SectionEntity;
using OLMS.Domain.Repositories;
using OLMS.Domain.Result;

public record UpdateQuizCommand(
    Guid QuizId,
    string Title,
    string Description,
    DateTime StartTime,
    DateTime EndTime,
    bool AllowLateSubmission,
    bool IsTimeLimited,
    TimeSpan? TimeLimit,
    int NumberOfAttempts,
    Guid InstructorId,
    Guid SectionId
) : IRequest<Result>;

public class UpdateQuizCommandHandler : IRequestHandler<UpdateQuizCommand, Result> {
    private readonly IQuizRepository _quizRepo;
    private readonly ISectionRepository _sectionRepository;
    private readonly ISectionItemRepository _sectionItemRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateQuizCommandHandler(
        IQuizRepository quizRepo,
        ISectionRepository sectionRepository,
        ISectionItemRepository sectionItemRepository,
        IUnitOfWork unitOfWork) {
        _quizRepo = quizRepo;
        _sectionRepository = sectionRepository;
        _sectionItemRepository = sectionItemRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(UpdateQuizCommand request, CancellationToken cancellationToken) {
        try {
            var quiz = await _quizRepo.GetByIdAsync(request.QuizId);
            if (quiz == null)
                return new Error("Quiz not found.");

            // Optionally, check if the instructor is allowed to update this quiz

            // Update quiz properties
            quiz.Update(
                request.Title,
                request.Description,
                request.StartTime,
                request.EndTime,
                request.AllowLateSubmission,
                request.IsTimeLimited,
                request.TimeLimit,
                request.NumberOfAttempts,
                request.InstructorId,
                request.SectionId
            );

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        } catch (DbUpdateException dbEx) {
            var innerMessage = dbEx.InnerException?.Message ?? dbEx.Message;
            throw new Exception($"Database error updating quiz: {innerMessage}", dbEx);
        } catch (Exception ex) {
            throw new Exception($"Error updating quiz: {ex.Message}", ex);
        }
    }
}

