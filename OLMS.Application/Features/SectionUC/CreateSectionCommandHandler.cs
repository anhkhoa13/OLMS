using MediatR;
using OLMS.Domain.Entities.SectionEntity;
using OLMS.Domain.Repositories;
using OLMS.Domain.Result;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace OLMS.Application.Features.CourseUC.SectionUC {
    public sealed record CreateSectionCommand(Guid CourseId, string Title) : IRequest<Result<Guid>>;

    public class CreateSectionCommandHandler : IRequestHandler<CreateSectionCommand, Result<Guid>> {
        private readonly ICourseRepository _courseRepository;
        private readonly ISectionRepository _sectionRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateSectionCommandHandler(
            ICourseRepository courseRepository,
            ISectionRepository sectionRepository,
            IUnitOfWork unitOfWork) {
            _courseRepository = courseRepository;
            _sectionRepository = sectionRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid>> Handle(
            CreateSectionCommand request,
            CancellationToken cancellationToken) {
            // Validate course exists
            var course = await _courseRepository.GetByIdAsync(request.CourseId, cancellationToken);
            if (course == null) {
                return Result<Guid>.Failure(new Error("Course not found"));
            }

            // Create new section
            var section = Section.Create(
                request.Title,
                request.CourseId
            );
            course.AddSection(section);
            // Persist section
            _courseRepository.Update(course); 
            await _sectionRepository.AddAsync(section, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result<Guid>.Success(section.Id);
        }
    }
}
