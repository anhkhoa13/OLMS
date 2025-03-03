using MediatR;
using OLMS.Domain.Entities;
using OLMS.Domain.Repositories;

namespace OLMS.Application.Feature.User;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Guid>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserRepository _userRepository;
    public CreateUserCommandHandler(IUnitOfWork unitOfWork, IUserRepository userRepository)
    {
        _unitOfWork = unitOfWork; 
        _userRepository = userRepository;
    }
    public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = UserBase.CreateUser(request.Id, request.Name, request.Email, request.Password, request.Role);

        await _userRepository.AddAsync(user);
        //await _unitOfWork.SaveChangesAsync(cancellationToken);
        return user.Id;
    }
}

