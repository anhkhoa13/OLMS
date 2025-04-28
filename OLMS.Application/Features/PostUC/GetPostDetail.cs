

using MediatR;
using OLMS.Domain.Entities.ForumAggregate.PostAggregate;
using OLMS.Domain.Repositories;
using OLMS.Domain.Result;

namespace OLMS.Application.Features.PostUC;

public sealed record GetPostDetailCommand(Guid PostId) : IRequest<Result<Post>>
{
}

public class GetPostDetailCommandHandler(IPostRepository postRepository) : IRequestHandler<GetPostDetailCommand, Result<Post>>
{
    private readonly IPostRepository _postRepository = postRepository;
    public async Task<Result<Post>> Handle(GetPostDetailCommand request, CancellationToken cancellationToken)
    {
        var post = await _postRepository.GetByIdAsync(request.PostId, cancellationToken);
        if (post is null)
        {
            return new Error("Post not found");
        }
        return post;
    }
}
