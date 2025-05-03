public sealed record LessonDetailsDto(
    Guid Id,
    string Title,
    string Content,
    string VideoUrl,
    Guid SectionId,
    List<LessonAttachmentDto> Attachments);