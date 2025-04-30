using OLMS.Domain.ValueObjects;

public record CourseDashboardDTO(Guid CourseId,
                        string Title,
                        string Code,
                        string InstructorName,
                        List<AssignmentDashboardDto> Assignments,
                        List<AnnouncementDashboardDto> Announcements
    ) { }
public record AnnouncementDashboardDto {
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public DateTime CreatedAt { get; set; }
}
public record AssignmentDashboardDto(
    Guid Id,
    string Title,
    DateTime DueDate,
    string type
    ) { }

