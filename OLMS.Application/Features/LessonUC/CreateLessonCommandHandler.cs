using MediatR;
using OLMS.Domain.Repositories;
using OLMS.Domain.Result;

public record CreateLessonCommand(
    string Title,
    string Content,
    string VideoUrl,
    Guid SectionId,
    List<AttachmentDto> Attachments
) : IRequest<Result>;

public class CreateLessonCommandHandler : IRequestHandler<CreateLessonCommand, Result> {
    private readonly ILessonRepository _lessonRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateLessonCommandHandler(
        ILessonRepository lessonRepository,
        IUnitOfWork unitOfWork) {
        _lessonRepository = lessonRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(CreateLessonCommand request, CancellationToken cancellationToken) {
        try {
            // Create the lesson entity
            var lesson = Lesson.Create(
                request.Title,
                request.Content,
                request.VideoUrl,
                request.SectionId);

            // Add attachments to the lesson
            if (request.Attachments != null && request.Attachments.Any()) {
                foreach (var attachmentDto in request.Attachments) {
                    var attachment = LessonAttachment.Create(
                        attachmentDto.Name,
                        attachmentDto.Data,
                        lesson.Id);

                    lesson.AddAttachment(attachment);
                }
            }

            // Save to repository
            await _lessonRepository.AddAsync(lesson, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        } catch (ArgumentException ex) {
            return Result.Failure(Error.Validation(ex.Message));
        } catch (Exception ex) {
            return Result.Failure(Error.Failure($"Failed to create lesson: {ex.Message}"));
        }
    }
}
