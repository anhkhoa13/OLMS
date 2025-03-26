using FluentValidation;
using MediatR;
using OLMS.Application.Feature.User;
using OLMS.Domain.Entities;
using OLMS.Domain.Repositories;
using OLMS.Domain.Result;
using OLMS.Domain.ValueObjects;

using static OLMS.Domain.Result.CourseError;

namespace OLMS.Application.Feature.CourseUC;

public sealed class CreateCourseValidation : AbstractValidator<CreateCourseCommand>
{
    public CreateCourseValidation()
    {
        Console.WriteLine("CreateCourseValidation is being executed...");
        RuleFor(c => c.Title).NotEmpty().WithMessage("Course name is required")
                            .Length(3, 100).WithMessage("Course name must be between 3 and 100 characters.");

        RuleFor(c => c.Description).MaximumLength(100);
    }
}
public sealed record CreateCourseCommand(string Title,
                                  string Description,
                                  Guid InstructorId) : IRequest<Result<Code>>;

public class CreateCourseCommandHandler : IRequestHandler<CreateCourseCommand, Result<Code>>
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
    public async Task<Result<Code>> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.InstructorId, cancellationToken);
        if (user is not Instructor instructor) 
            return InstructorNotFound;

        Course course = Course.Create(Guid.NewGuid(), 
                                      request.Title, 
                                      request.Description, 
                                      instructor);

        await _courseRepository.AddAsync(course);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return course.Code;   
    }
}
