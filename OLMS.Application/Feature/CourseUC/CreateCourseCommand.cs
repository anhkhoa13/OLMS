using MediatR;

namespace OLMS.Application.Feature.CourseUC;

public record CreateCourseCommand(string Title,
                                string Description,
                                Guid InstructorId) : IRequest<Guid>
{
}
