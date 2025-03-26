using MediatR;
using OLMS.Domain.Entities;

namespace OLMS.Application.Features.CourseUC
{
    public record DownloadMaterialQuery(Guid MaterialId) : IRequest<Material>;
}