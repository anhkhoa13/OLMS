

using MediatR;
using OLMS.Domain.Repositories;
using OLMS.Domain.Result;

namespace OLMS.Application.Features.CourseUC;

public record UpdateCourseCommand(Guid CourseId, string Title, string Description) : IRequest<Result>
{
}


public class UpdateCourseCommandHandler(ICourseRepository courseRepository, IUnitOfWork unitOfWork) : IRequestHandler<UpdateCourseCommand, Result>
{
    private readonly ICourseRepository _courseRepository = courseRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task<Result> Handle(UpdateCourseCommand request, CancellationToken cancellationToken)
    {
        var course = await _courseRepository.GetByIdAsync(request.CourseId, cancellationToken);
        if (course is null)
        {
            return new Error("Course not found");
        }

        course.Title = request.Title ?? throw new Exception("Title cannot be null");
        course.Description = request.Description;

        _courseRepository.Update(course);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}