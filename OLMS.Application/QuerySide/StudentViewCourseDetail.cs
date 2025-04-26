//using MediatR;
//using OLMS.Domain.Entities.CourseAggregate;
//using OLMS.Domain.Repositories;
//using OLMS.Domain.Result;

//namespace OLMS.Application.QuerySide;

//public sealed record StudentViewCourseDetailCommand(Guid StudentId, Guid ForumId) : IRequest<Result<IReadOnlyCollection<Course>>>
//{
//}

//public class StudentViewCourseDetailCommandHandler(IUserRepository userRepository, IProgressRepository progressRepository) : IRequestHandler<StudentViewCourseDetailCommand, Result<IReadOnlyCollection<Course>>>
//{
//    private readonly IProgressRepository _progressRepository = progressRepository;
//    private readonly IUserRepository _instuctorRepository = userRepository;

//    public async Task<Result<IReadOnlyCollection<Course>>> Handle(StudentViewCourseDetailCommand request, CancellationToken cancellationToken)
//    {
//        if (!await _instuctorRepository.IsStudent(request.StudentId))
//            return new Error("Invalid type role", "Not a student");

//        var courses = await _progressRepository.FindProgressesByStudentId(request.StudentId, cancellationToken);
//        return Result<IReadOnlyCollection<Course>>.Success(courses);
//    }
//}
