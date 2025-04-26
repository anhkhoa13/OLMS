using MediatR;
using OLMS.Domain.Entities.CourseAggregate;
using OLMS.Domain.Repositories;
using OLMS.Domain.Result;

using static OLMS.Domain.Result.CourseError;

namespace OLMS.Application.Features.StudentUC;
public sealed record GetAllCoursesCommand(Guid StudentId) : IRequest<Result<IReadOnlyCollection<Course>>>
{
}

public class GetAllCoursesCommandHandler(IStudentRepository studentRepository) : IRequestHandler<GetAllCoursesCommand, Result<IReadOnlyCollection<Course>>>
{
    private readonly IStudentRepository _studentRepository = studentRepository;

    public async Task<Result<IReadOnlyCollection<Course>>> Handle(GetAllCoursesCommand request, CancellationToken cancellationToken)
    {
        var courses = await _studentRepository.GetAllCourses(request.StudentId, cancellationToken);
        return Result<IReadOnlyCollection<Course>>.Success(courses);
    }
}