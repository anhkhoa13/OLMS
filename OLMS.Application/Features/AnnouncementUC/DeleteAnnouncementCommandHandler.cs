using MediatR;
using Microsoft.EntityFrameworkCore;
using OLMS.Domain.Repositories;
using OLMS.Domain.Result;

public record DeleteAnnouncementCommand(Guid AnnouncementId) : IRequest<Result>;

public class DeleteAnnouncementCommandHandler : IRequestHandler<DeleteAnnouncementCommand, Result> {
    private readonly IAnnouncementRepository _announcementRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteAnnouncementCommandHandler(
        IAnnouncementRepository announcementRepository,
        IUnitOfWork unitOfWork) {
        _announcementRepository = announcementRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(DeleteAnnouncementCommand request, CancellationToken cancellationToken) {
        try {
            var announcement = await _announcementRepository.GetByIdAsync(request.AnnouncementId);
            if (announcement == null)
                return Result.Failure(new Error("Announcement not found"));

            _announcementRepository.Delete(announcement);
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

