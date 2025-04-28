using MediatR;
using Microsoft.EntityFrameworkCore;
using OLMS.Domain.Entities.SectionEntity;
using OLMS.Domain.Repositories;
using OLMS.Domain.Result;

public sealed record GetSectionDetailsQuery(Guid CourseId)
    : IRequest<Result<List<SectionDto>>>;
public class GetSectionDetailsQueryHandler
    : IRequestHandler<GetSectionDetailsQuery, Result<List<SectionDto>>> {
    private readonly ISectionRepository _sectionRepository;

    public GetSectionDetailsQueryHandler(ISectionRepository sectionRepository) {
        _sectionRepository = sectionRepository;
    }

    public async Task<Result<List<SectionDto>>> Handle(
        GetSectionDetailsQuery request,
        CancellationToken cancellationToken) {
        var sections = await _sectionRepository.GetSectionsByCourseWithDetailsAsync(
            request.CourseId);

        if (!sections.Any()) {
            return Result<List<SectionDto>>.Failure(new Error("No sections found for this course"));
        }

        return Result<List<SectionDto>>.Success(sections.Select(MapToDto).ToList());
    }

    private static SectionDto MapToDto(Section section) {
        return new SectionDto(
            section.Id,
            section.Title,
            section.CourseId,
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
                a.DueDate
                )).ToList());
    }
}

// Repository method implementation

