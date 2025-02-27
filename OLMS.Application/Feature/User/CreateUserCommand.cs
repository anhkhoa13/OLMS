using MediatR;
using OLMS.Domain.Entities;

namespace OLMS.Application.Feature.User;

public record CreateUserCommand(Guid Id, 
                                string Name, 
                                string Email, 
                                string Password, 
                                Role Role) : IRequest<Guid>
{
}

