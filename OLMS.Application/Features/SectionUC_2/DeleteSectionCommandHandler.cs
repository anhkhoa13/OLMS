using MediatR;
using Microsoft.EntityFrameworkCore;
using OLMS.Domain.Repositories;
using OLMS.Domain.Result;

public record DeleteSectionCommand(Guid SectionId) : IRequest<Result>;

// DeleteSectionCommandHandler.cs
public class DeleteSectionCommandHandler
    : IRequestHandler<DeleteSectionCommand, Result> {
    private readonly ISectionRepository _sectionRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteSectionCommandHandler(
        ISectionRepository sectionRepository,
        IUnitOfWork unitOfWork) {
        _sectionRepository = sectionRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(
        DeleteSectionCommand request,
        CancellationToken cancellationToken) {
        try {
            var section = await _sectionRepository.GetByIdAsync(request.SectionId);
            if (section == null) {
                return Result.Failure(new Error("Section not found"));
            }
              
            // Check for dependent entities if needed
            //if (section.SectionItems?.Any() == true) {
            //    return Result.Failure(
            //        new Error("Cannot delete section with existing items. Delete items first."));
            //}

            _sectionRepository.Delete(section);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        } catch (DbUpdateException ex) {
            var innerMessage = ex.InnerException?.Message ?? ex.Message;
            return Result.Failure(new Error($"Database error: {innerMessage}"));
        } catch (Exception ex) {
            return Result.Failure(new Error(ex.Message));
        }
    }
}
