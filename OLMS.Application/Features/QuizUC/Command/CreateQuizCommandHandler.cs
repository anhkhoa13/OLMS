using MediatR;
using Microsoft.EntityFrameworkCore;
using OLMS.Domain.Entities.QuizEntity;
using OLMS.Domain.Entities.SectionEntity;
using OLMS.Domain.Repositories;
using OLMS.Domain.Result;

namespace OLMS.Application.Features.QuizUC.Command;
public record CreateQuizCommand(
    string Title,
    string Description,
    DateTime StartTime,
    DateTime EndTime,
    bool AllowLateSubmission,
    bool IsTimeLimited,
    TimeSpan? TimeLimit,
    int NumberOfAttempts,
    Guid InstructorId,
    Guid SectionId,
    int Order
    ) : IRequest<Result> {
}
public class CreateQuizCommandHandler : IRequestHandler<CreateQuizCommand, Result> {
    private readonly IQuizRepository _quizRepo;
    private readonly ISectionRepository _sectionRepository;
    private readonly ISectionItemRepository _sectionItemRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateQuizCommandHandler(IQuizRepository repository, ISectionRepository sectionRepository, ISectionItemRepository sectionItemRepository, IUnitOfWork unitOfWork) {
        _quizRepo = repository;
        _unitOfWork = unitOfWork;
        _sectionRepository = sectionRepository;
        _sectionItemRepository = sectionItemRepository;
    }

    public async Task<Result> Handle(CreateQuizCommand request, CancellationToken cancellationToken) {
        try {
            var quiz = Quiz.Create(
                request.Title,
                request.Description,
                request.StartTime,
                request.EndTime,
                request.AllowLateSubmission,
                request.IsTimeLimited,
                request.TimeLimit,
                request.NumberOfAttempts,
                request.InstructorId,
                request.SectionId
            );

            Section section = await _sectionRepository.GetByIdAsync(request.SectionId);
            if (section == null) {
                return new Error("section cannot be null");
            }
            var sectionItem = section.AddAssigment(quiz, request.Order);

            await _quizRepo.AddAsync(quiz);
            await _sectionItemRepository.AddAsync(sectionItem);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        } catch (DbUpdateException dbEx) {
            // Handle database-specific exceptions
            var innerMessage = dbEx.InnerException?.Message ?? dbEx.Message;
            throw new Exception($"Database error creating quiz: {innerMessage}", dbEx);
        } catch (Exception ex) {
            // Handle other exceptions
            throw new Exception($"Error creating quiz: {ex.Message}", ex);
        }
    }

}
