using MediatR;
using OLMS.Domain.Repositories;
using OLMS.Domain.Result;

namespace OLMS.Application.Features.LessonUC;

public record UpdateLessonCommand(Guid LessonId, string Title, string Content, string VideoUrl, List<AttachmentDto> Attachments) : IRequest<Result>
{
}

public class UpdateLessonCommandHandler(
    ILessonRepository lessonRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<UpdateLessonCommand, Result>
{
    private readonly ILessonRepository _lessonRepository = lessonRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result> Handle(UpdateLessonCommand request, CancellationToken cancellationToken)
    {
        var lesson = await _lessonRepository.GetLessonById(request.LessonId, cancellationToken);
        if (lesson == null)
        {
            return new Error("Lesson not found");
        }


        lesson.Title = request.Title ?? throw new Exception("Title cannot be null or empty");
        lesson.Content = request.Content ?? throw new Exception("Content cannot be null or empty");
        lesson.VideoUrl = request.VideoUrl ?? throw new Exception("VideoUrl cannot be null or empty");

        lesson.ClearAttachments();
        if (request.Attachments != null && request.Attachments.Count != 0)
        {
            foreach (var attachmentDto in request.Attachments)
            {
                var attachment = LessonAttachment.Create(
                    attachmentDto.Name,
                    attachmentDto.Data,
                    lesson.Id);

                lesson.AddAttachment(attachment);
            }
        }

        _lessonRepository.Update(lesson);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
