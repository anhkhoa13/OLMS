using MediatR;
using OLMS.Domain.Entities.CourseAggregate;
using OLMS.Domain.Repositories;
using OLMS.Domain.Result;

namespace OLMS.Application.Features.InstructorUC;
public sealed record GetAllCoursesCommand(Guid InstructorId) : IRequest<Result<IReadOnlyCollection<Course>>>
{
}

public class GetAllCoursesCommandHandler(IInstructorRepository intructorRepository) : IRequestHandler<GetAllCoursesCommand, Result<IReadOnlyCollection<Course>>>
{
    public async Task<Result<IReadOnlyCollection<Course>>> Handle(GetAllCoursesCommand request, CancellationToken cancellationToken)
    {
        var courses = await intructorRepository.GetAllCourses(request.InstructorId, cancellationToken);
        return Result<IReadOnlyCollection<Course>>.Success(courses);
    }
}