using MediatR;
using OLMS.Domain.Entities.SectionEntity;
using OLMS.Domain.Repositories;
using OLMS.Domain.Result;

public record CreateLessonCommand(
    string Title,
    string Content,
    string VideoUrl,
    Guid SectionId,
    List<AttachmentDto> Attachments,
    int order
) : IRequest<Result>;

public class CreateLessonCommandHandler : IRequestHandler<CreateLessonCommand, Result> {
    private readonly ILessonRepository _lessonRepository;
    private readonly ISectionRepository _sectionRepository;
    private readonly ISectionItemRepository _sectionItemRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateLessonCommandHandler(
        ILessonRepository lessonRepository,
        ISectionRepository sectionRepository,
        ISectionItemRepository sectionItemRepository,
        IUnitOfWork unitOfWork) {
        _sectionRepository = sectionRepository;
        _lessonRepository = lessonRepository;
        _unitOfWork = unitOfWork;
        _sectionItemRepository = sectionItemRepository;
    }

    public async Task<Result> Handle(CreateLessonCommand request, CancellationToken cancellationToken) {
        try {
            // Create the lesson entity
            var lesson = Lesson.Create(
                request.Title,
                request.Content,
                request.VideoUrl,
                request.SectionId);

            Section section = await _sectionRepository.GetByIdAsync(request.SectionId);
            if(section == null) {
                return new Error("section cannot be null");
            }
            var sectionItem = section.AddLesson(lesson, request.order);


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
            _sectionRepository.Update(section);
            await _lessonRepository.AddAsync(lesson, cancellationToken);
            await _sectionItemRepository.AddAsync(sectionItem, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        } catch (ArgumentException ex) {
            return Result.Failure(Error.Validation(ex.Message));
        } catch (Exception ex) {
            return Result.Failure(Error.Failure($"Failed to create lesson: {ex.Message}"));
        }
    }
}
