using MediatR;
using Microsoft.EntityFrameworkCore;
using OLMS.Domain.Repositories;
using OLMS.Domain.Result;

public record SectionOrderUpdateDto(
    Guid SectionId,
    int NewOrder
);

public record UpdateSectionOrderCommand(
    Guid CourseId,
    List<SectionOrderUpdateDto> Sections
) : IRequest<Result>;

public class UpdateSectionOrderCommandHandler
    : IRequestHandler<UpdateSectionOrderCommand, Result> {
    private readonly ISectionRepository _sectionRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateSectionOrderCommandHandler(
        ISectionRepository sectionRepository,
        IUnitOfWork unitOfWork) {
        _sectionRepository = sectionRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(
        UpdateSectionOrderCommand request,
        CancellationToken cancellationToken) {
        try {
            // Get all sections for the course
            var sections = await _sectionRepository
                .GetByCourseIdAsync(request.CourseId);

            // Validate all section IDs exist in the course
            var invalidIds = request.Sections
                .Select(s => s.SectionId)
                .Except(sections.Select(s => s.Id))
                .ToList();

            if (invalidIds.Any()) {
                return Result.Failure(
                   new Error($"Invalid section IDs: {string.Join(", ", invalidIds)}"));
            }

            // Create order mapping dictionary
            var orderMap = request.Sections
                .ToDictionary(s => s.SectionId, s => s.NewOrder);

            // Update section orders
            foreach (var section in sections) {
                if (orderMap.TryGetValue(section.Id, out var newOrder)) {
                    section.Order = newOrder;
                }
            }

            // Bulk update
            _sectionRepository.UpdateRange(sections);
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