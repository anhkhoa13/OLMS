public sealed record AssignmentDetailsDto(
    Guid Id,
    string Title,
    string Description,
    DateTime StartDate,
    DateTime DueDate,
    AssignmentType Type,
    bool AllowLateSubmission,
    int NumberOfAttempts,
    List<ExerciseAttachmentDto> Attachments);