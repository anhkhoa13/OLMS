using MediatR;
using OLMS.Domain.Entities;
using OLMS.Domain.Repositories;


namespace OLMS.Application.Feature.Enrollment;

public sealed record EnrollCourseCommand(Guid StudentId, string CourseCode) : IRequest;

public class EnrollCourseCommandHandler : IRequestHandler<EnrollCourseCommand>
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

    public async Task Handle(EnrollCourseCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.StudentId);

        if (user is null)
        {
            throw new InvalidOperationException("User not found");
        }

        if (user is not Student student)
        {
            throw new InvalidOperationException("Not a student");
        }

        var course = await _courseRepository.GetByCodeAsync(request.CourseCode);

        if (course == null)
        {
            throw new KeyNotFoundException("Invalid course code.");
        }

        course.EnrollStudent(student);

        _courseRepository.Update(course);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
