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
        try {
            var sections = await _sectionRepository.GetSectionsByCourseWithDetailsAsync(request.CourseId);

            var sectionDtos = sections.Select(MapToDto).ToList();
            return Result<List<SectionDto>>.Success(sectionDtos);
        } catch (Exception ex) {
            // Log the exception here if you have a logging framework
            // e.g., _logger.LogError(ex, "Error retrieving sections");

            return Result<List<SectionDto>>.Failure(
                new Error("SectionRetrievalError", $"An error occurred while retrieving sections: {ex.Message}")
            );
        }
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
                a.DueDate
                )).ToList());
    }
}

// Repository method implementation

