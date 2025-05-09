using MediatR;
using Microsoft.EntityFrameworkCore;
using OLMS.Domain.Repositories;
using OLMS.Domain.Result;

public record DeleteQuizCommand(Guid QuizId) : IRequest<Result>;

public class DeleteQuizCommandHandler : IRequestHandler<DeleteQuizCommand, Result> {
    private readonly IQuizRepository _quizRepository;
    private readonly ISectionItemRepository _sectionItemRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteQuizCommandHandler(
        IQuizRepository quizRepository,
        ISectionItemRepository sectionItemRepository,
        IUnitOfWork unitOfWork) {
        _quizRepository = quizRepository;
        _sectionItemRepository = sectionItemRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(DeleteQuizCommand request, CancellationToken cancellationToken) {
        try {
            var quiz = await _quizRepository.GetByIdAsync(request.QuizId);
            if (quiz == null)
                return Result.Failure(new Error("Quiz not found"));

            // Find the SectionItem for this Quiz
            var sectionItem = await _sectionItemRepository.GetByItemIdAsync(request.QuizId);
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

            // Deleting quiz will also delete questions if cascade is configured
            _quizRepository.Delete(quiz);
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

