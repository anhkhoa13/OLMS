using MediatR;
using OLMS.Domain.Entities;
using OLMS.Domain.Repositories;
using OLMS.Domain.Result;

using static OLMS.Domain.Result.CourseError;

namespace OLMS.Application.Features.Instructor;
public sealed record GetAllCoursesCommand(Guid instructorId) : IRequest<Result<IReadOnlyCollection<Course>>>
{
}

public class GetAllCoursesCommandHandler : IRequestHandler<GetAllCoursesCommand, Result<IReadOnlyCollection<Course>>>
{
    private readonly ICourseRepository _courseRepository;
    private readonly IUserRepository _userRepository;
    public GetAllCoursesCommandHandler(ICourseRepository courseRepository, IUserRepository userRepository)
    {
        _courseRepository = courseRepository;
        _userRepository = userRepository;
    }
    public async Task<Result<IReadOnlyCollection<Course>>> Handle(GetAllCoursesCommand request, CancellationToken cancellationToken)
    {
        var isInstructor = await _userRepository.IsInstructor(request.instructorId, cancellationToken);
        if (!isInstructor)
            return InstructorNotFound;
        var courses = await _courseRepository.FindCoursesByInstructorIdAsync(request.instructorId, cancellationToken);
        return Result<IReadOnlyCollection<Course>>.Success(courses);
    }
}