using MediatR;
using Microsoft.EntityFrameworkCore;
using OLMS.Domain.Entities.QuizEntity;
using OLMS.Domain.Repositories;

namespace OLMS.Application.Features.QuizUC.Command;
public record CreateQuizCommand(
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
    ) : IRequest<Guid> {
}
public class CreateQuizCommandHandler : IRequestHandler<CreateQuizCommand, Guid> {
    private readonly IQuizRepository _quizRepo;
    private readonly IUnitOfWork _unitOfWork;

    public CreateQuizCommandHandler(IQuizRepository repository, IUnitOfWork unitOfWork) {
        _quizRepo = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> Handle(CreateQuizCommand request, CancellationToken cancellationToken) {
        try {
            var quiz = Quiz.Create(
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

            await _quizRepo.AddAsync(quiz);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return quiz.Id;
        } catch (DbUpdateException dbEx) {
            // Handle database-specific exceptions
            var innerMessage = dbEx.InnerException?.Message ?? dbEx.Message;
            throw new Exception($"Database error creating quiz: {innerMessage}", dbEx);
        } catch (Exception ex) {
            // Handle other exceptions
            throw new Exception($"Error creating quiz: {ex.Message}", ex);
        }
    }

}
