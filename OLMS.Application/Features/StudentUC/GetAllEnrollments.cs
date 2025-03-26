using MediatR;
using OLMS.Domain.Entities;
using OLMS.Domain.Repositories;
using OLMS.Domain.Result;

namespace OLMS.Application.Features.StudentUC;

public sealed record GetAllEnrollmentsCommand(Guid StudentId) : IRequest<Result<IReadOnlyCollection<Course>>>
{
}

public class GetAllEnrollmentCommandHandler : IRequestHandler<GetAllEnrollmentsCommand, Result<IReadOnlyCollection<Course>>>
{
    private readonly ICourseRepository _courseRepository;
    private readonly IUserRepository _userRepository;
    public GetAllEnrollmentCommandHandler(IUserRepository userRepository, ICourseRepository courseRepository)
    {
        _courseRepository = courseRepository;
        _userRepository = userRepository;
    }
    public async Task<Result<IReadOnlyCollection<Course>>> Handle(GetAllEnrollmentsCommand request, CancellationToken cancellationToken)
    {
        if (!await _userRepository.IsStudent(request.StudentId))
            return new Error("Invalid type role", "Not a student");

        var courses = await _courseRepository.GetAllCourseEnroll(request.StudentId, cancellationToken);
        return Result<IReadOnlyCollection<Course>>.Success(courses);
    }
}
