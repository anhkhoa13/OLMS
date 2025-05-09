using MediatR;
using Microsoft.EntityFrameworkCore;
using OLMS.Domain.Repositories;
using OLMS.Domain.Result;

public record DeleteLessonCommand(Guid LessonId) : IRequest<Result>;

public class DeleteLessonCommandHandler : IRequestHandler<DeleteLessonCommand, Result> {
    private readonly ILessonRepository _lessonRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteLessonCommandHandler(
        ILessonRepository lessonRepository,
        IUnitOfWork unitOfWork) {
        _lessonRepository = lessonRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(DeleteLessonCommand request, CancellationToken cancellationToken) {
        try {
            var lesson = await _lessonRepository.GetLessonById(request.LessonId);
            if (lesson == null)
                return Result.Failure(new Error("Lesson not found"));

            // Deleting lesson will also delete attachments if cascade is configured
            _lessonRepository.Delete(lesson);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        } catch (DbUpdateException dbEx) {
            var innerMessage = dbEx.InnerException?.Message ?? dbEx.Message;
            return Result.Failure(new Error($"Database error: {innerMessage}"));
        } catch (Exception ex) {
            return Result.Failure(new Error(ex.Message));
        }
    }
}

