using MediatR;
using OLMS.Domain.Entities;
using OLMS.Domain.Repositories;

namespace OLMS.Application.Feature.CourseUC;

public class CreateCourseCommandHandler : IRequestHandler<CreateCourseCommand, Guid>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICourseRepository _courseRepository;
    private readonly IUserRepository _userRepository;

    public CreateCourseCommandHandler(IUnitOfWork unitOfWork, ICourseRepository courseRepository, IUserRepository userRepository)
    {
        _unitOfWork = unitOfWork;
        _courseRepository = courseRepository;
        _userRepository = userRepository;   
    }
    public async Task<Guid> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.InstructorId, cancellationToken);
        if (user is not Instructor instructor)
        {
            throw new Exception("Invalid instructor.");
        }
        Course course = Course.Create(request.Id, request.Title, request.Description, instructor);
        await _courseRepository.AddAsync(course);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return course.Id;   
    }
}
