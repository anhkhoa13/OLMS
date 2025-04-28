
using MediatR;
using OLMS.Domain.Repositories;
using OLMS.Domain.Result;

using static OLMS.Domain.Result.Result;

namespace OLMS.Application.Features.PostUC;

public sealed record DownvotePostCommand(Guid PostId, Guid userId) : IRequest<Result>
{
}

public class DownvotePostCommandHandler(IUnitOfWork unitOfWork, IPostRepository postRepository, IVoteRepository voteRepository) : IRequestHandler<DownvotePostCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IPostRepository _postRepository = postRepository;
    private readonly IVoteRepository _voteRepository = voteRepository;

    public async Task<Result> Handle(DownvotePostCommand request, CancellationToken cancellationToken)
    {
        var post = await _postRepository.GetByIdAsync(request.PostId, cancellationToken);
        if (post is null)
        {
            return new Error("Post not found");
        }
        var vote = post.DownVote(request.userId);

        if (vote is not null)
        {
            await _voteRepository.AddAsync(vote, cancellationToken);
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Success();
    }
}
