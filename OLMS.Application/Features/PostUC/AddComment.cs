

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
    public async Task<Result> Handle(
    AddCommentCommand request,
    CancellationToken cancellationToken) {
        try {
            var post = await _postRepository.GetByIdAsync(request.PostId, cancellationToken);
            if (post is null) {
                return Result.Failure(new Error("Post.NotFound", "Post not found"));
            }

            var comment = post.AddComment(request.Content, request.UserId);
            await _commentRepository.AddAsync(comment, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Result.Success();
        } catch (Exception ex) {
            // Log the exception here if you have logging configured
            // _logger.LogError(ex, "Error adding comment to post {PostId}", request.PostId);

            return Result.Failure(
                new Error("Comment.CreationFailed",
                $"Failed to add comment: {ex.Message}")
            );
        }
    }

}
