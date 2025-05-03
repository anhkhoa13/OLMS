using MediatR;
using OLMS.Domain.Repositories;
using OLMS.Domain.Result;

public record CreateAnnouncementCommand(string Title, string Content, Guid CourseId) : IRequest<Result<Guid>>;

public class CreateAnnouncementCommandHandler : IRequestHandler<CreateAnnouncementCommand, Result<Guid>> {
    private readonly IAnnouncementRepository _announcementRepository;
    private readonly ICourseRepository _courseRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateAnnouncementCommandHandler(
        IAnnouncementRepository announcementRepository,
        ICourseRepository courseRepository,
        IUnitOfWork unitOfWork) {
        _announcementRepository = announcementRepository;
        _courseRepository = courseRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(CreateAnnouncementCommand request, CancellationToken cancellationToken) {
        try {
            // Validate if course exists
            var course = await _courseRepository.GetByIdAsync(request.CourseId);
            if (course == null) {
                return Result<Guid>.Failure(new Error("CourseNotFound", "The specified course does not exist."));
            }

            // Create the announcement
            var announcement = Announcement.CreateAnnouncement(
                request.Title,
                request.Content,
                request.CourseId);

            // Add to repository
            await _announcementRepository.AddAsync(announcement);

            // Save changes
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result<Guid>.Success(announcement.Id);
        } catch (Exception ex) {
            return Result<Guid>.Failure(new Error("AnnouncementCreationFailed", ex.Message));
        }
    }
}