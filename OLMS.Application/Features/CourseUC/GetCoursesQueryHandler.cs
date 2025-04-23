

using MediatR;
using OLMS.Domain.Entities;

public record GetCoursesQuery : IRequest<List<Course>> {
    public string UserID { get; set; }
}
public class GetCoursesQueryHandler : IRequestHandler<GetCoursesQuery, List<Course>> {
    public Task<List<Course>> Handle(GetCoursesQuery request, CancellationToken cancellationToken) {
        throw new NotImplementedException();
    }
}

