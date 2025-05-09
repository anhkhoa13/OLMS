using MediatR;
using Microsoft.EntityFrameworkCore;
using OLMS.Domain.Repositories;
using OLMS.Domain.Result;

public record DeleteLessonCommand(Guid LessonId) : IRequest<Result>;

public class DeleteLessonCommandHandler : IRequestHandler<DeleteLessonCommand, Result> {
    private readonly ILessonRepository _lessonRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISectionItemRepository _sectionItemRepository;
    

    public DeleteLessonCommandHandler(
        ILessonRepository lessonRepository,
        IUnitOfWork unitOfWork,
        ISectionItemRepository sectionItemRepository) {
        _lessonRepository = lessonRepository;
        _unitOfWork = unitOfWork;
        _sectionItemRepository = sectionItemRepository;
    }

    public async Task<Result> Handle(DeleteLessonCommand request, CancellationToken cancellationToken) {
        try {
            var lesson = await _lessonRepository.GetLessonById(request.LessonId);
            if (lesson == null)
                return Result.Failure(new Error("Lesson not found"));

            // Since only one SectionItem corresponds to one ItemId, fetch a single item
            var sectionItem = await _sectionItemRepository.GetByItemIdAsync(request.LessonId);

                
            if (sectionItem == null)
                return Result.Failure(new Error("sectionItem not found"));


            // Get all items in the same section with a higher order
            var itemsToUpdate = await _sectionItemRepository
                .GetBySectionIdAndOrderGreaterThanAsync(sectionItem.SectionId, sectionItem.Order);

            // Decrement their order
            foreach (var item in itemsToUpdate) {
                item.DecreaseOrder();
            }

            if (sectionItem != null) {
                _sectionItemRepository.Delete(sectionItem);
            }
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

