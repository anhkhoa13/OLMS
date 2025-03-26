using MediatR;
using OLMS.Domain.Entities;
using OLMS.Domain.Repositories;

namespace OLMS.Application.Features.CourseUC
{
    public class DownloadMaterialQueryHandler : IRequestHandler<DownloadMaterialQuery, Material>
    {
        private readonly IMaterialRepository _materialRepository;

        public DownloadMaterialQueryHandler(IMaterialRepository materialRepository)
        {
            _materialRepository = materialRepository;
        }

        public async Task<Material> Handle(DownloadMaterialQuery request, CancellationToken cancellationToken)
        {
            var material = await _materialRepository.GetByIdAsync(request.MaterialId);
            if (material == null)
            {
                throw new Exception("Material not found.");
            }

            return material;
        }
    }
}