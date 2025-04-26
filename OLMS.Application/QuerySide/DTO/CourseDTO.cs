namespace OLMS.Application.QuerySide.DTO;

public record CourseDTO(Guid CourseId,
                        string Title,
                        string Description,
                        Guid InstructorId)
{ }

