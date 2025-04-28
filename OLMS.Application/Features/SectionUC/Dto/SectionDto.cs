public sealed record SectionDto(
    Guid Id,
    string Title,
    Guid CourseId,
    int Order,
    List<LessonDto> Lessons,
    List<SectionItemDto> SectionItems,
    List<AssignmentDto> Assignments);

public sealed record LessonDto(Guid Id, string Title);
public sealed record SectionItemDto(Guid Id, int Order, SectionItemType ItemType, Guid ItemId);
public sealed record AssignmentDto(Guid Id, string Title, AssignmentType Type, DateTime DueDate);
