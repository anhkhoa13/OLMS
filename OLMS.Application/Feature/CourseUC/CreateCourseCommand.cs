using MediatR;

namespace OLMS.Application.Feature.CourseUC;

public record CreateCourseCommand(Guid Id,
                                string Title,
                                string Description,
                                Guid InstructorId) : IRequest<Guid>
{
}
