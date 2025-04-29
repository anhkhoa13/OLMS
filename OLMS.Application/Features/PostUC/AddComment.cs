

using MediatR;
using OLMS.Domain.Repositories;
using OLMS.Domain.Result;

namespace OLMS.Application.Features.PostUC;

public sealed record AddCommentCommand(Guid PostId, Guid UserId, string Content) : IRequest<Result>
{
}

public class AddCommentCommandHandler(IUnitOfWork unitOfWork, IPostRepository postRepository, ICommentRepository commmentRepository) : IRequestHandler<AddCommentCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IPostRepository _postRepository = postRepository;
    private readonly ICommentRepository _commentRepository = commmentRepository;
    public async Task<Result> Handle(AddCommentCommand request, CancellationToken cancellationToken)
    {
        var post = await _postRepository.GetByIdAsync(request.PostId, cancellationToken);
        if (post is null)
        {
            return new Error("Post not found");
        }

        var comment = post.AddComment(request.Content, request.UserId);
        await _commentRepository.AddAsync(comment, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}
