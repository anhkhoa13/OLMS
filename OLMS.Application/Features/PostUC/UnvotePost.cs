

using MediatR;
using OLMS.Domain.Repositories;
using OLMS.Domain.Result;

namespace OLMS.Application.Features.PostUC;

public sealed record UnvotePost(Guid PostId, Guid userId) : IRequest<Result>
{
}

public class UnvotePostHandler(IUnitOfWork unitOfWork, IPostRepository postRepository, IVoteRepository voteRepository) : IRequestHandler<UnvotePost, Result>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IPostRepository _postRepository = postRepository;
    private readonly IVoteRepository _voteRepository = voteRepository;
    public async Task<Result> Handle(UnvotePost request, CancellationToken cancellationToken)
    {
        var post = await _postRepository.GetByIdAsync(request.PostId, cancellationToken);
        if (post is null)
        {
            return new Error("Post not found");
        }
        post.UnVote(request.userId);
        //if (vote is not null)
        //{
        //    await _voteRepository.RemoveAsync(vote, cancellationToken);
        //}
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}