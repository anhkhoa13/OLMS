//using MediatR;
//using OLMS.Application.QuerySide.DTO;
//using OLMS.Application.Repositories.Read;
//using OLMS.Domain.Result;

//namespace OLMS.Application.QuerySide;

//public sealed record StudentGetAllCoursesCommand(Guid studentId) : IRequest<Result<IReadOnlyCollection<CourseDTO>>>
//{
//}

//public class StudentGetAllCourseCommandHandler(ICourseReadRepository courseReadRepository) : IRequestHandler<StudentGetAllCoursesCommand, Result<IReadOnlyCollection<CourseDTO>>>
//{
//    public async Task<Result<IReadOnlyCollection<CourseDTO>>> Handle(StudentGetAllCoursesCommand request, CancellationToken cancellationToken)
//    {
//        var courses = await courseReadRepository.GetCoursesByStudentIdAsync(request.studentId, cancellationToken);
//        return Result<IReadOnlyCollection<CourseDTO>>.Success(courses);
//    }
//}