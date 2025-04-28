using MediatR;
using OLMS.Domain.Entities.SectionEntity;
using OLMS.Domain.Repositories;
using OLMS.Domain.Result;

public record GetLessonQuery(
    Guid LessonId
) : IRequest<Result<LessonDetailsDto>>;

public class GetLessonQueryHandler : IRequestHandler<GetLessonQuery, Result<LessonDetailsDto>> {
    private readonly ILessonRepository _lessonRepository;

    public GetLessonQueryHandler(
        ILessonRepository lessonRepository) {
        _lessonRepository = lessonRepository;
    }

    public async Task<Result<LessonDetailsDto>> Handle(
    GetLessonQuery request,
    CancellationToken cancellationToken) {
        try {
            var lesson = await _lessonRepository.GetLessonById(request.LessonId);
            if (lesson == null) {
                return Result<LessonDetailsDto>.Failure(new Error("Lesson not found"));
            }

            var dto = MapToDto(lesson);
            return Result<LessonDetailsDto>.Success(dto);
        } catch (Exception ex) {
            return Result<LessonDetailsDto>.Failure(Error.Failure($"Failed to get lesson: {ex.Message}"));
        }
    }

    private static LessonDetailsDto MapToDto(Lesson lesson) {
        return new LessonDetailsDto(
            lesson.Id,
            lesson.Title,
            lesson.Content,
            lesson.VideoUrl,
            lesson.SectionId,
            lesson.LessonAttachments.Select(la => new LessonAttachmentDto(
                la.Id,
                la.Name,
                la.Type,
                la.Data)).ToList());
    }

}
