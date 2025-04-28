using MediatR;
using Microsoft.EntityFrameworkCore;
using OLMS.Domain.Result;
using OLMS.Domain.Entities.ForumAggregate;
using System.Threading;
using System.Threading.Tasks;
using OLMS.Domain.Repositories;

public sealed record GetForumCommand(Guid CourseId) : IRequest<Result<ForumDto>>;

public class GetForumCommandHandler : IRequestHandler<GetForumCommand, Result<ForumDto>> {
    private readonly IForumRepository _forumRepository;

    public GetForumCommandHandler(IForumRepository forumRepository) {
        _forumRepository = forumRepository;
    }

    public async Task<Result<ForumDto>> Handle(GetForumCommand request, CancellationToken cancellationToken) {
        var forum = await _forumRepository.GetForumWithPostsByCourseIdAsync(request.CourseId, cancellationToken);

        if (forum == null)
            return Result<ForumDto>.Failure(new Error("Forum not found for the specified course."));

        var forumDto = new ForumDto {
            Id = forum.Id,
            Title = forum.Title,
            CourseId = forum.CourseId,
            Posts = forum.Posts.Select(post => new PostDto {
                Id = post.Id,
                Title = post.Title,
                Body = post.Body,
                VoteScore = post.VoteScore,
                Comments = post.Comments.Select(c => new CommentDto {
                    Id = c.Id,
                    Content = c.Content,
                    PostId = c.PostId,
                    UserId = c.UserId
                }).ToList(),
                Votes = post.Votes.Select(v => new VoteDto {
                    Id = v.Id,
                    Type = v.Type,
                    PostId = v.PostId,
                    UserId = v.UserId
                }).ToList()
            }).ToList()
        };

        return Result<ForumDto>.Success(forumDto);
    }
}

