using MediatR;
using Microsoft.EntityFrameworkCore;
using OLMS.Domain.Entities.SectionEntity;
using OLMS.Domain.Repositories;
using OLMS.Domain.Result;

public sealed record GetSectionItemQuery(Guid SectionId)
    : IRequest<Result<SectionDto>>;
public class GetSectionItemQueryHandler
    : IRequestHandler<GetSectionItemQuery, Result<SectionDto>> {
    private readonly ISectionRepository _sectionRepository;

    public GetSectionItemQueryHandler(ISectionRepository sectionRepository) {
        _sectionRepository = sectionRepository;
    }

    public async Task<Result<SectionDto>> Handle(
    GetSectionItemQuery request,
    CancellationToken cancellationToken) {
        var section = await _sectionRepository.GetSectionById(request.SectionId);

        if (section == null) {
            return new Error("Section not found");
        }

        return Result<SectionDto>.Success(MapToDto(section));
    }

    private static SectionDto MapToDto(Section section) {
        return new SectionDto(
            section.Id,
            section.Title,
            section.CourseId,
            section.Order,
            section.Lessons.Select(l => new LessonDto(l.Id, l.Title)).ToList(),
            section.SectionItems.Select(si => new SectionItemDto(
                si.Id,
                si.Order,
                si.ItemType,
                si.ItemId)).ToList(),
            section.Assignments.Select(a => new AssignmentDto(
                a.Id,
                a.Title,
                a.Type,
                a.DueDate)).ToList());
    }

}

// Repository method implementation

