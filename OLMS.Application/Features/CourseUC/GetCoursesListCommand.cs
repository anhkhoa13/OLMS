using MediatR;
using OLMS.Domain.Entities;
using OLMS.Domain.Entities.CourseAggregate;
using OLMS.Domain.Repositories;
using OLMS.Domain.Result;

using static OLMS.Domain.Result.CourseError;

namespace OLMS.Application.Features.CourseUC;
public sealed record GetCoursesListCommand() : IRequest<Result<IReadOnlyCollection<Course>>> {
}

public class GetCoursesListCommandHandler : IRequestHandler<GetCoursesListCommand, Result<IReadOnlyCollection<Course>>> {
    private readonly ICourseRepository _courseRepository;
    public GetCoursesListCommandHandler(ICourseRepository courseRepository) {
        _courseRepository = courseRepository;
    }
    public async Task<Result<IReadOnlyCollection<Course>>> Handle(GetCoursesListCommand request, CancellationToken cancellationToken) {
        var courses = await _courseRepository.GetAllAsync(cancellationToken);
        return Result<IReadOnlyCollection<Course>>.Success(courses.ToList().AsReadOnly());
    }
}