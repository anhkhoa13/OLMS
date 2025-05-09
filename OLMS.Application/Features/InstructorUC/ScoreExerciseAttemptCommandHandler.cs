using MediatR;
using Microsoft.EntityFrameworkCore;
using OLMS.Domain.Repositories;
using OLMS.Domain.Result;

public record ScoreExerciseAttemptCommand(
    Guid ExerciseAttemptId,
    float Score
) : IRequest<Result>;

public class ScoreExerciseAttemptCommandHandler : IRequestHandler<ScoreExerciseAttemptCommand, Result> {
    private readonly IExerciseAttemptRepository _attemptRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ScoreExerciseAttemptCommandHandler(
        IExerciseAttemptRepository attemptRepository,
        IUnitOfWork unitOfWork) {
        _attemptRepository = attemptRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(ScoreExerciseAttemptCommand request, CancellationToken cancellationToken) {
        try {
            var attempt = await _attemptRepository.GetByIdAsync(request.ExerciseAttemptId);
            if (attempt == null)
                return Result.Failure(new Error("Exercise attempt not found."));

            attempt.SetScore(request.Score); // Use a domain method to set score and status
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        } catch (DbUpdateException dbEx) {
            var innerMessage = dbEx.InnerException?.Message ?? dbEx.Message;
            return Result.Failure(new Error($"Database error: {innerMessage}"));
        } catch (Exception ex) {
            return Result.Failure(new Error(ex.Message));
        }
    }
}
