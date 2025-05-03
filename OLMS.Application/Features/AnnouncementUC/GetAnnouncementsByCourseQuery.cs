using MediatR;
using OLMS.Domain.Repositories;
using OLMS.Domain.Result;

public record GetAnnouncementsByCourseQuery(Guid CourseId) : IRequest<Result<List<AnnouncementDto>>>;

public class GetAnnouncementsByCourseQueryHandler
        : IRequestHandler<GetAnnouncementsByCourseQuery, Result<List<AnnouncementDto>>> {
    private readonly IAnnouncementRepository _announcementRepository;
    private readonly ICourseRepository _courseRepository;

    public GetAnnouncementsByCourseQueryHandler(
        IAnnouncementRepository announcementRepository,
        ICourseRepository courseRepository) {
        _announcementRepository = announcementRepository;
        _courseRepository = courseRepository;
    }

    public async Task<Result<List<AnnouncementDto>>> Handle(
        GetAnnouncementsByCourseQuery request,
        CancellationToken cancellationToken) {
        try {
            // Verify course exists
            var courseExists = await _courseRepository.GetByIdAsync(request.CourseId);
            if (courseExists == null) {
                return Result<List<AnnouncementDto>>.Failure(
                    new Error("CourseNotFound", "The specified course does not exist."));
            }

            // Get announcements
            var announcements = await _announcementRepository.GetByCourseIdAsync(request.CourseId);

            // Map to DTOs
            var announcementDtos = announcements.Select(a => new AnnouncementDto {
                Id = a.Id,
                Title = a.Title,
                Content = a.Content,
                CreatedAt = a.CreatedAt,
                CourseId = a.CourseId
            }).ToList();

            return Result<List<AnnouncementDto>>.Success(announcementDtos);
        } catch (Exception ex) {
            return Result<List<AnnouncementDto>>.Failure(
                new Error("AnnouncementRetrievalFailed", ex.Message));
        }
    }
}