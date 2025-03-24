using MediatR;

namespace OLMS.Application.Features.CourseUC
{
    public record UploadMaterialCommand(
        Guid CourseId,
        string Name,
        string ContentType,
        byte[] Data
    ) : IRequest<Guid>;
}