using MediatR;
using OLMS.Domain.Repositories;
using OLMS.Domain.Result;

public record CreateExerciseCommand(
    string Title,
    string Description,
    DateTime StartDate,
    DateTime DueDate,
    bool AllowLateSubmission,
    int NumberOfAttempts,
    Guid SectionId,
    Guid InstructorId,
    List<AttachmentDto> Attachments
) : IRequest<Result>;

public class CreateAssignmentCommandHandler : IRequestHandler<CreateExerciseCommand, Result> {
    private readonly IAssignmentRepository _assignmentRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateAssignmentCommandHandler(
        IAssignmentRepository assignmentRepository,
        IUnitOfWork unitOfWork) {
        _assignmentRepository = assignmentRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(CreateExerciseCommand request, CancellationToken cancellationToken) {
        try {
            var exercise = Exercise.Create(
                request.Title,
                request.Description,
                request.StartDate,
                request.DueDate,
                request.NumberOfAttempts,
                request.AllowLateSubmission,
                request.SectionId,
                request.InstructorId
                );

            // Add attachments to the lesson
            if (request.Attachments != null && request.Attachments.Any()) {
                foreach (var attachmentDto in request.Attachments) {
                    var attachment = ExerciseAttachment.Create(
                        attachmentDto.Name,
                        attachmentDto.Data,
                        exercise.Id);

                    exercise.AddAttachment(attachment);
                }
            }

            // Save to repository
            await _assignmentRepository.AddAsync(exercise, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        } catch (ArgumentException ex) {
            return Result.Failure(Error.Validation(ex.Message));
        } catch (Exception ex) {
            return Result.Failure(Error.Failure($"Failed to create lesson: {ex.Message}"));
        }
    }
}
