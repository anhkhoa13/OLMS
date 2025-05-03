// GetDashboardQuery.cs
using MediatR;
using OLMS.Domain.Entities.QuizEntity;
using OLMS.Domain.Repositories;
using OLMS.Domain.Result;

public record GetDashboardQuery(Guid studentId) : IRequest<Result<List<CourseDashboardDTO>>>;


public class GetDashboardQueryHandler : IRequestHandler<GetDashboardQuery, Result<List<CourseDashboardDTO>>> {
    private readonly ICourseRepository _courseRepository;
    private readonly IInstructorRepository _instructorRepository;

    public GetDashboardQueryHandler(
        ICourseRepository courseRepository,
        IInstructorRepository instructorRepository) {
        _courseRepository = courseRepository;
        _instructorRepository = instructorRepository;
    }

    public async Task<Result<List<CourseDashboardDTO>>> Handle(
        GetDashboardQuery request,
        CancellationToken cancellationToken) {
        var courses = await _courseRepository.GetCoursesByStudentIdAsync(request.studentId, cancellationToken);
        var courseDtos = new List<CourseDashboardDTO>();

        foreach (var course in courses) {
            var instructor = await _instructorRepository.GetByIdAsync(course.InstructorId, cancellationToken);

            // Get all assignments across all sections
            var allAssignments = course.Sections
                .SelectMany(s => s.Assignments)
                .Select(a => new AssignmentDashboardDto(
                    a.Id,
                    a.Title,
                    a.DueDate,
                    a is Quiz ? "quiz" : "exercise"
                ))
                .ToList();

            var announcements = course.Announcements.Select(a => new AnnouncementDashboardDto {
                Id = a.Id,
                Title = a.Title,
                Content = a.Content,
                CreatedAt = a.CreatedAt
            }).ToList();

            var courseDto = new CourseDashboardDTO(
                course.Id,
                course.Title,
                course.Code.Value,
                instructor?.FullName.Value ?? "Unknown Instructor",
                allAssignments,  // Now passing flat list of assignments
                announcements
            );

            courseDtos.Add(courseDto);
        }

        return Result<List<CourseDashboardDTO>>.Success(courseDtos);
    }
}