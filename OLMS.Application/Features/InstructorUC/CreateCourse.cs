using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OLMS.Domain.Entities.InstructorAggregate;
using OLMS.Domain.Repositories;
using OLMS.Domain.Result;
using OLMS.Domain.ValueObjects;

using static OLMS.Domain.Result.CourseError;

namespace OLMS.Application.Features.InstructorUC;

public sealed class CreateCourseValidation : AbstractValidator<CreateCourseCommand>
{
    public CreateCourseValidation()
    {
        RuleFor(c => c.Title).NotEmpty().WithMessage("Course name is required")
                            .Length(3, 100).WithMessage("Course name must be between 3 and 100 characters.");

        RuleFor(c => c.Description).MaximumLength(100);
    }
}
public sealed record CreateCourseCommand(string Title,
                                  string Description,
                                  Guid InstructorId) : IRequest<Result<Code>>;

public class CreateCourseCommandHandler(IUnitOfWork unitOfWork, IInstructorRepository instructorRepository, ICourseRepository courseRepository) : IRequestHandler<CreateCourseCommand, Result<Code>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IInstructorRepository _instuctorRepository = instructorRepository;
    private readonly ICourseRepository _courseRepository = courseRepository;

    public async Task<Result<Code>> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
    {
        var user = await _instuctorRepository.GetByIdAsync(request.InstructorId, cancellationToken);
        if (user is not Instructor instructor)
            return InstructorNotFound;

        var course = instructor.CreateCourse(request.Title, request.Description);

        _instuctorRepository.Update(instructor);
        await _courseRepository.AddAsync(course, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return course.Code;
    }
}
