using MediatR;
using OLMS.Domain.Entities;
using OLMS.Domain.Repositories;
using OLMS.Domain.ValueObjects;

using static OLMS.Domain.Error.Error.User;

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
        var username = Username.Create(request.Username);

        if (!await _userRepository.IsUsernameUniqueAsync(username, cancellationToken))
            throw new ArgumentException(NonUniqueUsername);

        var email = Email.Create(request.Email);

        if (!await _userRepository.IsEmailUniqueAsync(email, cancellationToken))
            throw new ArgumentException(NonUniqueEmail);

        var password = Password.Create(request.Password);
        var fullName = FullName.Create(request.FullName);

        var user = UserBase.CreateUser(Guid.NewGuid(), 
                                       username, 
                                       password, 
                                       fullName,
                                       email, 
                                       request.Age,
                                       request.Role);

        await _userRepository.AddAsync(user);
        //await _unitOfWork.SaveChangesAsync(cancellationToken);
        return user.Id;
    }
}

