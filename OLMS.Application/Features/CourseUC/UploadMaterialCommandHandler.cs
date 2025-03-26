using MediatR;
using OLMS.Domain.Entities;
using OLMS.Domain.Repositories;

namespace OLMS.Application.Features.CourseUC
{
    public class UploadMaterialCommandHandler : IRequestHandler<UploadMaterialCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMaterialRepository _materialRepository;
        private readonly ICourseMaterialRepository _courseMaterialRepository;

        public UploadMaterialCommandHandler(
            IUnitOfWork unitOfWork,
            IMaterialRepository materialRepository,
            ICourseMaterialRepository courseMaterialRepository)
        {
            _unitOfWork = unitOfWork;
            _materialRepository = materialRepository;
            _courseMaterialRepository = courseMaterialRepository;
        }

        public async Task<Guid> Handle(UploadMaterialCommand request, CancellationToken cancellationToken)
        {
            var material = new Material
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                ContentType = request.ContentType,
                Data = request.Data
            };

            await _materialRepository.AddAsync(material);

            var courseMaterial = new CourseMaterial
            {
                CourseId = request.CourseId,
                MaterialId = material.Id
            };

            await _courseMaterialRepository.AddAsync(courseMaterial);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return material.Id;
        }
    }
}