public sealed record LessonAttachmentDto(
    Guid Id,
    string Name,
    string Type,
    byte[] Data);