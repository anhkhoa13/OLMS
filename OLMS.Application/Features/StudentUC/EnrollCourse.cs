using MediatR;
using OLMS.Domain.Repositories;
using OLMS.Domain.Result;
using OLMS.Domain.Entities;

using static OLMS.Domain.Result.Result;
using static OLMS.Domain.Result.UserError;
using static OLMS.Domain.Result.CourseError;


namespace OLMS.Application.Features.StudentUC;

public sealed record EnrollCourseCommand(Guid StudentId, string CourseCode) : IRequest<Result>;

public class EnrollCourseCommandHandler : IRequestHandler<EnrollCourseCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserRepository _userRepository;
    private readonly ICourseRepository _courseRepository;

    public EnrollCourseCommandHandler(
        IUnitOfWork unitOfWork,
        IUserRepository userRepository,
        ICourseRepository courseRepository)
    {
        _unitOfWork = unitOfWork;
        _userRepository = userRepository;
        _courseRepository = courseRepository;
    }

    public async Task<Result> Handle(EnrollCourseCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.StudentId);

        if (user is null)
            return UserNotFound;

        if (user is not Student student)
            return InvalidRole;

        var course = await _courseRepository.GetByCodeAsync(request.CourseCode, cancellationToken);

        if (course is null)
            return CourseNotFound;

        course.EnrollStudent(student);

        _courseRepository.Update(course);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Success();
    }
}
