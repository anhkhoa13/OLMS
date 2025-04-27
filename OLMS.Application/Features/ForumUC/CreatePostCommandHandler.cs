using MediatR;
using Microsoft.Extensions.Logging;
using OLMS.Domain.Result;
using OLMS.Domain.Entities.ForumAggregate.PostAggregate;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace OLMS.Application.Features.ForumUC.PostUC;

public sealed record CreatePostCommand(string Title, string Body, Guid ForumId, Guid UserId) : IRequest<Result<PostDto>>;

public class CreatePostCommandHandler : IRequestHandler<CreatePostCommand, Result<PostDto>> {
    private readonly IPostRepository _postRepository;
    private readonly ILogger<CreatePostCommandHandler> _logger;

    public CreatePostCommandHandler(IPostRepository postRepository, ILogger<CreatePostCommandHandler> logger) {
        _postRepository = postRepository;
        _logger = logger;
    }

    public async Task<Result<PostDto>> Handle(CreatePostCommand request, CancellationToken cancellationToken) {
        // Input validation
        if (string.IsNullOrWhiteSpace(request.Title))
            return Result<PostDto>.Failure(new Error("Post title is required."));

        if (string.IsNullOrWhiteSpace(request.Body))
            return Result<PostDto>.Failure(new Error("Post body is required."));

        // Verify the forum exists
        if (!await _postRepository.ForumExistsAsync(request.ForumId, cancellationToken))
            return Result<PostDto>.Failure(new Error("The specified forum does not exist."));

        try {
            // Create post entity using the factory method
            var post = Post.Create(request.Title, request.Body, request.ForumId);

            // Save to repository
            var createdPost = await _postRepository.CreatePostAsync(post, cancellationToken);

            // Log the creation action with user ID for auditing
            _logger.LogInformation("Post {PostId} created by user {UserId}", createdPost.Id, request.UserId);

            // Map to DTO
            var postDto = new PostDto {
                Id = createdPost.Id,
                Title = createdPost.Title,
                Body = createdPost.Body,
                VoteScore = createdPost.VoteScore,
                ForumId = createdPost.ForumId
            };

            return Result<PostDto>.Success(postDto);
        } catch (Exception ex) {
            _logger.LogError(ex, "Error creating post by user {UserId} for forum {ForumId}",
                request.UserId, request.ForumId);

            return Result<PostDto>.Failure(new Error($"Failed to create post: {ex.Message}"));
        }
    }
}
