using MediatR;
using OLMS.Domain.Entities;
using OLMS.Domain.Entities.CourseAggregate;
using OLMS.Domain.Repositories;
using OLMS.Domain.Result;

using static OLMS.Domain.Result.CourseError;

namespace OLMS.Application.Features.CourseUC;
public sealed record GetEnrollingCoursesQuery() : IRequest<Result<IReadOnlyCollection<Course>>> {
}

public class GetEnrollingCoursesQueryHandler : IRequestHandler<GetEnrollingCoursesQuery, Result<IReadOnlyCollection<Course>>> {
    private readonly ICourseRepository _courseRepository;
    public GetEnrollingCoursesQueryHandler(ICourseRepository courseRepository) {
        _courseRepository = courseRepository;
    }
    public async Task<Result<IReadOnlyCollection<Course>>> Handle(GetEnrollingCoursesQuery request, CancellationToken cancellationToken) {
        var courses = await _courseRepository.GetAllEnrollingCourses();
        if(courses==null) {
            return new Error("cannot find any courses");
        }
        return Result<IReadOnlyCollection<Course>>.Success(courses.ToList().AsReadOnly());
    }
}