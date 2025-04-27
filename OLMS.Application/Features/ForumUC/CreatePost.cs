
using MediatR;
using OLMS.Domain.Repositories;
using OLMS.Domain.Result;

using static OLMS.Domain.Result.Result;

namespace OLMS.Application.Features.ForumUC;

public sealed record CreatePostCommand(Guid forumId, string title, string body) : IRequest<Result>
{
}

public class CreatePostCommandHandler(IForumRepository forumRepository, IUnitOfWork unitOfWork, IPostRepository postRepository) : IRequestHandler<CreatePostCommand, Result>
{
    private readonly IForumRepository _forumRepository = forumRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IPostRepository _postRepository = postRepository;
    public async Task<Result> Handle(CreatePostCommand request, CancellationToken cancellationToken)
    {
        var forum = await _forumRepository.GetByIdAsync(request.forumId, cancellationToken);
        if (forum is null)
        {
            return new Error("Forum not found");
        }

        var post = forum.CreatePost(request.title, request.body);

        await _postRepository.AddAsync(post, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Success();
    }
}
