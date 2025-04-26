using MediatR;
using OLMS.Domain.Result;

namespace OLMS.Application.Features.ForumUC;

public sealed record CreateForumCommand(Guid CourseId) : IRequest<Result> {

}
public class CreateForumCommandHandler : IRequestHandler<CreateForumCommand, Result> {
    public Task<Result> Handle(CreateForumCommand request, CancellationToken cancellationToken) {
        throw new NotImplementedException();
    }
}

