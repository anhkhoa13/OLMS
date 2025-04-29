using MediatR;
using OLMS.Domain.Entities.ForumAggregate;
using OLMS.Domain.Entities.ForumAggregate.PostAggregate;
using OLMS.Domain.Repositories;
using OLMS.Domain.Result;

namespace OLMS.Application.Features.ForumUC;

public sealed record GetForumsDetailCommand(Guid forumId) : IRequest<Result<Forum>> {

}
public class GetForumsDetailCommandHandler(IForumRepository forumRepository) : IRequestHandler<GetForumsDetailCommand, Result<Forum>>
{
    private readonly IForumRepository _forumRepository = forumRepository;
    public Task<Result<Forum>> Handle(GetForumsDetailCommand request, CancellationToken cancellationToken)
    {
        return _forumRepository.GetByIdAsync(request.forumId, cancellationToken)
            .ContinueWith(task =>
            {
                if (task.Result is null)
                {
                    return Result<Forum>.Failure(new Error("Forum not found", "FORUM_NOT_FOUND"));
                }

                return task.Result;
            }, cancellationToken);
    }
}

