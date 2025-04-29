//using MediatR;
//using OLMS.Application.QuerySide.DTO;
//using OLMS.Application.Repositories.Read;
//using OLMS.Domain.ExerciseAttempt;

//namespace OLMS.Application.QuerySide;

//public sealed record StudentGetAllCoursesCommand(Guid studentId) : IRequest<ExerciseAttempt<IReadOnlyCollection<CourseDTO>>>
//{
//}

//public class StudentGetAllCourseCommandHandler(ICourseReadRepository courseReadRepository) : IRequestHandler<StudentGetAllCoursesCommand, ExerciseAttempt<IReadOnlyCollection<CourseDTO>>>
//{
//    public async Task<ExerciseAttempt<IReadOnlyCollection<CourseDTO>>> Handle(StudentGetAllCoursesCommand request, CancellationToken cancellationToken)
//    {
//        var courses = await courseReadRepository.GetCoursesByStudentIdAsync(request.studentId, cancellationToken);
//        return ExerciseAttempt<IReadOnlyCollection<CourseDTO>>.Success(courses);
//    }
//}