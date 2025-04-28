public sealed record ExerciseAttachmentDto(
    Guid Id,
    string Name,
    string Type,
    byte[] Data);