using MediatR;

namespace OLMS.Application.Feature.CourseUC
{
    public record CreateCourseCommand(
        string Title,
        string Description,
        string Code,
        Guid InstructorId
    ) : IRequest<Guid>;
}
