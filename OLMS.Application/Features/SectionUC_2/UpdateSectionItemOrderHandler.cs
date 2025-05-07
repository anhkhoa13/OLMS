using MediatR;
using Microsoft.EntityFrameworkCore;
using OLMS.Domain.Repositories;
using OLMS.Domain.Result;

public record UpdateSectionItemOrderCommand(
    Guid SectionId,
    List<SectionItemOrderUpdateDto> Items
) : IRequest<Result>;

public record SectionItemOrderUpdateDto(
    Guid SectionItemId,
    int NewOrder
);

// Command Handler
public class UpdateSectionItemOrderHandler
    : IRequestHandler<UpdateSectionItemOrderCommand, Result> {
    private readonly ISectionItemRepository _sectionItemRepo;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateSectionItemOrderHandler(
        ISectionItemRepository sectionItemRepo,
        IUnitOfWork unitOfWork) {
        _sectionItemRepo = sectionItemRepo;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(
        UpdateSectionItemOrderCommand request,
        CancellationToken cancellationToken) {
        try {
            // Get all section items for the section
            var sectionItems = await _sectionItemRepo
                .GetBySectionIdAsync(request.SectionId);

            // Validate all section item IDs belong to this section
            var invalidIds = request.Items
                .Select(i => i.SectionItemId)
                .Except(sectionItems.Select(si => si.Id))
                .ToList();

            if (invalidIds.Any()) {
                return Result.Failure( new Error(
                    $"Invalid section item IDs: {string.Join(", ", invalidIds)}"));
            }

            // Create order mapping
            var orderMap = request.Items
                .ToDictionary(i => i.SectionItemId, i => i.NewOrder);

            // Update order for each item
            foreach (var sectionItem in sectionItems) {
                if (orderMap.TryGetValue(sectionItem.Id, out var newOrder)) {
                    sectionItem.Order = newOrder;
                }
            }

            // Bulk update
            _sectionItemRepo.UpdateRange(sectionItems);
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
