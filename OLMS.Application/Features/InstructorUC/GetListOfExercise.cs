using MediatR;
using OLMS.Domain.Repositories;
using OLMS.Domain.Result;

namespace OLMS.Application.Features.InstructorUC;

public record ExerciseAttemptDTO(
    Guid ExerciseAttemptId,
    Guid ExerciseId,
    Guid StudentId,
    string StudentName,
    float Score,
    DateTime SubmitAt,
    Enum Status,
    List<AttachmentDTO> Attachments);

public sealed record AttachmentDTO(
    Guid Id,
    string Name,
    string Type,
    byte[] Data);

public record GetListOfExerciseCommand(Guid ExerciseId) : IRequest<Result<List<ExerciseAttemptDTO>>>
{
}

public class GetListOfExerciseCommandHandler(
    IExerciseAttemptRepository exerciseAttemptRepository,
    IUserRepository userRepository) : IRequestHandler<GetListOfExerciseCommand, Result<List<ExerciseAttemptDTO>>>
{
    private readonly IExerciseAttemptRepository _exerciseAttemptRepository = exerciseAttemptRepository;
    public async Task<Result<List<ExerciseAttemptDTO>>> Handle(GetListOfExerciseCommand request, CancellationToken cancellationToken)
    {
        var attempts = await _exerciseAttemptRepository.GetAllByExerciseId(request.ExerciseId, cancellationToken);
        if (attempts == null || attempts.Count == 0)
        {
            return new Error("No attempts found for the given exercise.");
        }

        var studentIds = attempts.Select(a => a.StudentId).Distinct();
        var students = await userRepository.GetUsersByIdsAsync(studentIds, cancellationToken);

        var studentDict = students.ToDictionary(u => u.Id, u => u.FullName.Value);

        var attemptDtos = attempts.Select(attempt =>
        {
            var studentName = studentDict.TryGetValue(attempt.StudentId, out var name) ? name : "Unknown";
            return new ExerciseAttemptDTO(
                attempt.Id,
                attempt.ExerciseId,
                attempt.StudentId,
                studentName,
                attempt.Score,
                attempt.SubmitAt,
                attempt.Status,
                attempt.SubmitAttachtment.Select(a => new AttachmentDTO(a.Id, a.Name, a.Type, a.Data)).ToList()
            );
        }).ToList();

        return Result<List<ExerciseAttemptDTO>>.Success(attemptDtos);
    }
}