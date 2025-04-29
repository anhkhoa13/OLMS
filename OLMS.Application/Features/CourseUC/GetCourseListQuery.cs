using MediatR;
using OLMS.Domain.Entities;
using OLMS.Domain.Entities.CourseAggregate;
using OLMS.Domain.Repositories;
using OLMS.Domain.Result;

using static OLMS.Domain.Result.CourseError;

namespace OLMS.Application.Features.CourseUC;
public sealed record GetCourseListQuery() : IRequest<Result<IReadOnlyCollection<Course>>> {
}

public class GetCourseListQueryHandler : IRequestHandler<GetCourseListQuery, Result<IReadOnlyCollection<Course>>> {
    private readonly ICourseRepository _courseRepository;
    public GetCourseListQueryHandler(ICourseRepository courseRepository) {
        _courseRepository = courseRepository;
    }
    public async Task<Result<IReadOnlyCollection<Course>>> Handle(GetCourseListQuery request, CancellationToken cancellationToken) {
        var courses = await _courseRepository.GetAllAsync(cancellationToken);
        if(courses==null) {
            return new Error("cannot find any courses");
        }
        return Result<IReadOnlyCollection<Course>>.Success(courses.ToList().AsReadOnly());
    }
}