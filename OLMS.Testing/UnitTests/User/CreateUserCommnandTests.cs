using FluentAssertions;
using NSubstitute;
using OLMS.Application.Feature.User;
using OLMS.Domain.Entities;
using OLMS.Domain.Repositories;
using OLMS.Domain.ValueObjects;
using Xunit;

using static OLMS.Domain.Error.Error.User;

namespace OLMS.Testing.UnitTests.User;

public class CreateUserCommnandTests
{
    private static readonly CreateUserCommand CMD = new("tTestId", "Testing123@", "Teacher 1", "teacher.testing@mail.com", Role.Instructor);

    private readonly CreateUserCommandHandler _handler;
    private readonly IUnitOfWork _unitOfWorkMock;
    private readonly IUserRepository _userRepositoryMock;

    public CreateUserCommnandTests()
    {
        _unitOfWorkMock = Substitute.For<IUnitOfWork>();
        _userRepositoryMock = Substitute.For<IUserRepository>();
        _handler = new CreateUserCommandHandler(_unitOfWorkMock, _userRepositoryMock);
    }

    [Theory]
    [InlineData("")]
    [InlineData("       ")]
    public async Task Handle_Should_ReturnError_WhenMissingUsername(string username)
    {
        //Arrange
        var cmd = CMD with { Username = username }; 

        //Act
        Func<Task> act = async () => await _handler.Handle(cmd, default);

        //Assert
        await act.Should().ThrowAsync<ArgumentException>().WithMessage(EmptyUsername);
        await _userRepositoryMock.Received(0).AddAsync(Arg.Any<UserBase>());
        await _unitOfWorkMock.Received(0).SaveChangesAsync(Arg.Any<CancellationToken>());
    }

    [Theory]
    [InlineData("")]
    [InlineData("       ")]
    public async Task Handle_Should_ReturnError_WhenMissingFullName(string fullname)
    {
        //Arrange
        var cmd = CMD with { FullName = fullname };
        _userRepositoryMock.IsEmailUniqueAsync(Arg.Is<Email>(e => e.Value == CMD.Email))
            .Returns(true);
        _userRepositoryMock.IsUsernameUniqueAsync(Arg.Is<Username>(u => u.Value == CMD.Username))
            .Returns(true);

        //Act
        Func<Task> act = async () => await _handler.Handle(cmd, default);

        //Assert
        await act.Should().ThrowAsync<ArgumentException>().WithMessage(EmptyName);
        await _userRepositoryMock.Received(0).AddAsync(Arg.Any<UserBase>());
        await _unitOfWorkMock.Received(0).SaveChangesAsync(Arg.Any<CancellationToken>());
    }

    [Theory]
    [InlineData("")]
    [InlineData("       ")]
    public async Task Handle_Should_ReturnError_WhenMissingEmail(string email)
    {
        //Arrange
        var cmd = CMD with { Email = email };
        _userRepositoryMock.IsEmailUniqueAsync(Arg.Is<Email>(e => e.Value == CMD.Email))
            .Returns(true);
        _userRepositoryMock.IsUsernameUniqueAsync(Arg.Is<Username>(u => u.Value == CMD.Username))
            .Returns(true);

        //Act
        Func<Task> act = async () => await _handler.Handle(cmd, default);

        //Assert
        await act.Should().ThrowAsync<ArgumentException>().WithMessage(EmptyEmail);
        await _userRepositoryMock.Received(0).AddAsync(Arg.Any<UserBase>());
        await _unitOfWorkMock.Received(0).SaveChangesAsync(Arg.Any<CancellationToken>());
    }

    [Theory]
    [InlineData("")]
    [InlineData("       ")]
    public async Task Handle_Should_ReturnError_WhenMissingPassword(string password)
    {
        //Arrange
        var cmd = CMD with { Password = password };
        _userRepositoryMock.IsEmailUniqueAsync(Arg.Is<Email>(e => e.Value == CMD.Email))
            .Returns(true);
        _userRepositoryMock.IsUsernameUniqueAsync(Arg.Is<Username>(u => u.Value == CMD.Username))
            .Returns(true);

        //Act
        Func<Task> act = async () => await _handler.Handle(cmd, default);

        //Assert
        await act.Should().ThrowAsync<ArgumentException>().WithMessage(EmptyPassword);
        await _userRepositoryMock.Received(0).AddAsync(Arg.Any<UserBase>());
        await _unitOfWorkMock.Received(0).SaveChangesAsync(Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task Handle_Should_AddUser_WhenValidData()
    {
        //Arrange
        var cmd = CMD;
        _userRepositoryMock.IsEmailUniqueAsync(Arg.Is<Email>(e => e.Value == CMD.Email))
            .Returns(true);
        _userRepositoryMock.IsUsernameUniqueAsync(Arg.Is<Username>(u => u.Value == CMD.Username))
            .Returns(true);

        //Act
        var act = await _handler.Handle(cmd, default);

        //Assert
        act.Should().NotBeEmpty();
        await _userRepositoryMock.Received(1).AddAsync(Arg.Is<UserBase>(u =>
                u.Id == act &&
                u.Username.Value == cmd.Username &&
                u.Password.Value == cmd.Password &&
                u.FullName.Value == cmd.FullName &&
                u.Email.Value == cmd.Email &&
                u.Role == cmd.Role
        ));
        await _unitOfWorkMock.Received(1).SaveChangesAsync(Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task Handle_Should_ReturnError_WhenEmailNotUnique()
    {
        //Arrange
        _userRepositoryMock.IsEmailUniqueAsync(Arg.Is<Email>(e => e.Value == CMD.Email))
            .Returns(false);

        _userRepositoryMock.IsUsernameUniqueAsync(Arg.Is<Username>(u => u.Value == CMD.Username))
            .Returns(true);


        //Act
        Func<Task> act = async () => await _handler.Handle(CMD, default);

        //Assert
        await act.Should().ThrowAsync<ArgumentException>().WithMessage(NonUniqueEmail);
        await _userRepositoryMock.Received(0).AddAsync(Arg.Any<UserBase>());
        await _unitOfWorkMock.Received(0).SaveChangesAsync(Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task Handle_Should_ReturnError_WhenUsernameNotUnique()
    {
        //Arrange
        _userRepositoryMock.IsUsernameUniqueAsync(Arg.Is<Username>(u => u.Value == CMD.Username))
            .Returns(false);

        _userRepositoryMock.IsEmailUniqueAsync(Arg.Is<Email>(e => e.Value == CMD.Email))
            .Returns(true);

        //Act
        Func<Task> act = async () => await _handler.Handle(CMD, default);

        //Assert
        await act.Should().ThrowAsync<ArgumentException>().WithMessage(NonUniqueUsername);
        await _userRepositoryMock.Received(0).AddAsync(Arg.Any<UserBase>());
        await _unitOfWorkMock.Received(0).SaveChangesAsync(Arg.Any<CancellationToken>());
    }
}
