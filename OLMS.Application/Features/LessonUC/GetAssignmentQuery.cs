using MediatR;
using OLMS.Domain.Entities.SectionEntity;
using OLMS.Domain.Repositories;
using OLMS.Domain.Result;

// only get for assignment type exercise
public record GetAssignmentQuery(
    Guid ExerciseId
) : IRequest<Result<AssignmentDetailsDto>>;

public class GetAssignmentQueryHandler
    : IRequestHandler<GetAssignmentQuery, Result<AssignmentDetailsDto>> {
    private readonly IExerciseRepository _exerciseRepository;

    public GetAssignmentQueryHandler(IExerciseRepository exerciseRepository) {
        _exerciseRepository = exerciseRepository;
    }

    public async Task<Result<AssignmentDetailsDto>> Handle(
        GetAssignmentQuery request,
        CancellationToken cancellationToken) {
        try {
            var assignment = await _exerciseRepository.GetByIdAsync(request.ExerciseId);

            if (assignment == null) {
                return Result<AssignmentDetailsDto>.Failure(
                    new Error("Assignment not found"));
            }

            return Result<AssignmentDetailsDto>.Success(MapToDto(assignment));
        } catch (Exception ex) {
            return Result<AssignmentDetailsDto>.Failure(
                Error.Failure($"Failed to get assignment: {ex.Message}"));
        }
    }

    private static AssignmentDetailsDto MapToDto(Exercise exercise) {
        return new AssignmentDetailsDto(
            exercise.Id,
            exercise.Title,
            exercise.Description,
            exercise.StartDate,
            exercise.DueDate,
            exercise.Type,
            exercise.AllowLateSubmission,
            exercise.NumberOfAttempts,
            exercise.ExerciseAttachments.Select(ea => new ExerciseAttachmentDto(
                ea.Id,
                ea.Name,
                ea.Type,
                ea.Data)).ToList());
    }
}


