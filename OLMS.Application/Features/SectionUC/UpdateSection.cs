using MediatR;
using OLMS.Domain.Repositories;
using OLMS.Domain.Result;

namespace OLMS.Application.Features.SectionUC;

public record UpdateSectionCommand(Guid SectionId, string Title, int Order) : IRequest<Result>
{
}

public class UpdateSectionCommandHandler(
    ISectionRepository sectionRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<UpdateSectionCommand, Result>
{
    private readonly ISectionRepository _sectionRepository = sectionRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task<Result> Handle(UpdateSectionCommand request, CancellationToken cancellationToken)
    {
        var section = await _sectionRepository.GetByIdAsync(request.SectionId, cancellationToken);
        if (section == null)
        {
            return new Error("Section not found");
        }
        section.Title = request.Title ?? throw new Exception("Title cannot be null or empty");
        section.Order = request.Order;
        _sectionRepository.Update(section);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}