
using MediatR;
using OLMS.Domain.Repositories;
using OLMS.Domain.Result;

namespace OLMS.Application.Features.AdminUC;

public sealed record ApproveCourseCommand(Guid CourseId, Guid AdminId) : IRequest<Result>
{
}

public class ApproveCourseCommandHandler(ICourseRepository courseRepository, IAdminRepository adminRepository, IUnitOfWork unitOfWork) : IRequestHandler<ApproveCourseCommand, Result>
{
    private readonly ICourseRepository _courseRepository = courseRepository;
    private readonly IAdminRepository _adminRepository = adminRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task<Result> Handle(ApproveCourseCommand request, CancellationToken cancellationToken)
    {
        var course = await _courseRepository.GetByIdAsync(request.CourseId, cancellationToken);
        if (course is null)
        {
            return new Error("Course not found.");
        }
        var admin = await _adminRepository.GetByIdAsync(request.AdminId, cancellationToken);
        if (admin is null)
        {
            return new Error("You are not admin");
        }
        admin.ApproveCourse(course);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}

