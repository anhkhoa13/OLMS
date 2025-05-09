using MediatR;
using Microsoft.EntityFrameworkCore;
using OLMS.Domain.Repositories;
using OLMS.Domain.Result;

public record DeletePostCommand(Guid PostId) : IRequest<Result>;

public class DeletePostCommandHandler : IRequestHandler<DeletePostCommand, Result> {
    private readonly IPostRepository _postRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeletePostCommandHandler(IPostRepository postRepository, IUnitOfWork unitOfWork) {
        _postRepository = postRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(DeletePostCommand request, CancellationToken cancellationToken) {
        try {
            var post = await _postRepository.GetByIdAsync(request.PostId);
            if (post == null)
                return Result.Failure(new Error("Post not found"));

            _postRepository.Delete(post);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        } catch (DbUpdateException dbEx) {
            var innerMessage = dbEx.InnerException?.Message ?? dbEx.Message;
            return Result.Failure(new Error($"Database error: {innerMessage}"));
        } catch (Exception ex) {
            return Result.Failure(new Error(ex.Message));
        }
    }
}
