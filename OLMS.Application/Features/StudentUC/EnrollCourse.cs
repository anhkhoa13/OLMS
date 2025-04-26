using MediatR;
using OLMS.Domain.Repositories;
using OLMS.Domain.Result;
using static OLMS.Domain.Result.Result;
using static OLMS.Domain.Result.UserError;
using static OLMS.Domain.Result.CourseError;
using OLMS.Domain.Entities.StudentAggregate;


namespace OLMS.Application.Features.StudentUC;

public sealed record EnrollCourseCommand(Guid StudentId, string CourseCode) : IRequest<Result>;

public class EnrollCourseCommandHandler(
    IUnitOfWork unitOfWork,
    IUserRepository userRepository,
    ICourseRepository courseRepository,
    IProgressRepository progressRepository) : IRequestHandler<EnrollCourseCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IUserRepository _userRepository = userRepository;
    private readonly ICourseRepository _courseRepository = courseRepository;
    private readonly IProgressRepository _progressRepository = progressRepository;

    public async Task<Result> Handle(EnrollCourseCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.StudentId, cancellationToken);

        if (user is null)
            return UserNotFound;

        if (user is not Student student)
            return InvalidRole;

        var course = await _courseRepository.GetByCodeAsync(request.CourseCode, cancellationToken);

        if (course is null)
            return CourseNotFound;

        var progress = student.EnrollCourse(course);

        await _progressRepository.AddAsync(progress, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Success();
    }
}