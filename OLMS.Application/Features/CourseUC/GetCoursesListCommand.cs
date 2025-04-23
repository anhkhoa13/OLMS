using MediatR;
using OLMS.Domain.Entities;
using OLMS.Domain.Repositories;
using OLMS.Domain.Result;

using static OLMS.Domain.Result.CourseError;

namespace OLMS.Application.Features.CourseUC;
public sealed record GetCoursesListCommand() : IRequest<Result<IReadOnlyCollection<Course>>> {
}

public class GetCoursesListCommandHandler : IRequestHandler<GetCoursesListCommand, Result<IReadOnlyCollection<Course>>> {
    private readonly ICourseRepository _courseRepository;
    private readonly IUserRepository _userRepository;
    public GetCoursesListCommandHandler(ICourseRepository courseRepository, IUserRepository userRepository) {
        _courseRepository = courseRepository;
        _userRepository = userRepository;
    }
    public async Task<Result<IReadOnlyCollection<Course>>> Handle(GetCoursesListCommand request, CancellationToken cancellationToken) {
        var courses = await _courseRepository.GetAllAsync(cancellationToken);
        return Result<IReadOnlyCollection<Course>>.Success(courses.ToList().AsReadOnly());
    }
}