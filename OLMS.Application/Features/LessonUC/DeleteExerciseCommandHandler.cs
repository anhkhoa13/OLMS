using MediatR;
using Microsoft.EntityFrameworkCore;
using OLMS.Domain.Repositories;
using OLMS.Domain.Result;

public record DeleteExerciseCommand(Guid ExerciseId) : IRequest<Result>;

public class DeleteExerciseCommandHandler : IRequestHandler<DeleteExerciseCommand, Result> {
    private readonly IExerciseRepository _exerciseRepository;
    private readonly ISectionItemRepository _sectionItemRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteExerciseCommandHandler(
        IExerciseRepository exerciseRepository,
        ISectionItemRepository sectionItemRepository,
        IUnitOfWork unitOfWork) {
        _exerciseRepository = exerciseRepository;
        _sectionItemRepository = sectionItemRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(DeleteExerciseCommand request, CancellationToken cancellationToken) {
        try {
            var exercise = await _exerciseRepository.GetByIdWithAttachmentsAsync(request.ExerciseId);
            if (exercise == null)
                return Result.Failure(new Error("Exercise not found"));

            // Find the SectionItem for this Exercise
            var sectionItem = await _sectionItemRepository.GetByItemIdAsync(request.ExerciseId);
            if (sectionItem != null) {
                var deletedOrder = sectionItem.Order;
                var sectionId = sectionItem.SectionId;

                // Delete the SectionItem
                _sectionItemRepository.Delete(sectionItem);

                // Reorder remaining SectionItems in the section
                var itemsToUpdate = await _sectionItemRepository.GetBySectionIdAndOrderGreaterThanAsync(sectionId, deletedOrder);
                foreach (var item in itemsToUpdate) {
                    item.Order -= 1;
                }
            }

            // Deleting exercise will also delete attachments if cascade is configured
            _exerciseRepository.Delete(exercise);
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

