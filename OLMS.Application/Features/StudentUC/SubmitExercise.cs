using MediatR;
using OLMS.Domain.Entities.AssignmentAttempt;
using OLMS.Domain.Repositories;
using OLMS.Domain.Result;

namespace OLMS.Application.Features.StudentUC;

public record SubmitExerciseCommand(Guid StudentId, Guid ExerciseId, List<AttachmentDto> Attachments) : IRequest<Result>
{
}

public class SubmitExerciseCommandHandler(
    IExerciseAttemptRepository exerciseAttemptRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<SubmitExerciseCommand, Result>
{
    private readonly IExerciseAttemptRepository _exerciseAttemptRepository = exerciseAttemptRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result> Handle(SubmitExerciseCommand request, CancellationToken cancellationToken)
    {
        try {
            var existingAttempt = await _exerciseAttemptRepository.FindByExerciseIdAndStudentId(request.ExerciseId, request.StudentId, cancellationToken);
            //Console.WriteLine(existingAttempt == null ? "Attempt is null" : $"Attempt ID: {existingAttempt.Id}");
            if (existingAttempt is not null) {
                return new Error("You already do the exam");
            }
            var exerciseAttempt = new ExerciseAttempt(Guid.NewGuid(), DateTime.UtcNow, request.ExerciseId, request.StudentId);

            foreach (var attachmentDto in request.Attachments) {
                var attachment = SubmitAttachment.Create(
                    attachmentDto.Name,
                    attachmentDto.Data,
                    exerciseAttempt.Id);
                exerciseAttempt.AddAttachment(attachment);
            }

            await _exerciseAttemptRepository.AddAsync(exerciseAttempt, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Result.Success();
        } catch(Exception ex) {
            return new Error(ex.Message);
        }  
    }
}

