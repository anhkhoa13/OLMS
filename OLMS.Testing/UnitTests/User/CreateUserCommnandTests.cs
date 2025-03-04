using FluentAssertions;
using NSubstitute;
using OLMS.Application.Feature.User;
using OLMS.Domain.Entities;
using OLMS.Domain.Repositories;
using Xunit;

using static OLMS.Domain.Error.Error.User;

namespace OLMS.Testing.UnitTests.User;

public class CreateUserCommnandTests
{
    private static readonly CreateUserCommand CMD = new("tTestId", "Teacher 1", "teacher.testing@mail.com", "testing123", Role.Instructor);

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
    public async Task Handle_Should_ReturnError_WhenMissingLoginId(string loginId)
    {
        //Arrange
        var cmd = CMD with { Username = loginId }; 

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
    public async Task Handle_Should_ReturnError_WhenMissingName(string name)
    {
        //Arrange
        var cmd = CMD with { Name = name };

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

        //Act
        Func<Task> act = async () => await _handler.Handle(cmd, default);

        //Assert
        await act.Should().ThrowAsync<ArgumentException>().WithMessage(EmptyPassword);
        await _userRepositoryMock.Received(0).AddAsync(Arg.Any<UserBase>());
        await _unitOfWorkMock.Received(0).SaveChangesAsync(Arg.Any<CancellationToken>());
    }

    //    [Fact]
    //    public async Task Handle_Should_AddUser_WhenValidData()
    //    {
    //        //Arrange
    //        var cmd = CMD;

    //        //Act
    //        var result = await _handler.Handle(cmd, default);

    //        //Assert
    //        result.Should().NotBeEmpty();
    //        await _userRepositoryMock.Received(1).AddAsync(Arg.Is<UserBase>(u =>
    //            //u.Id == result &&
    //            //u.Username == cmd.Username &&
    //            //u.Password == cmd.Password &&
    //            //u.FullName == cmd.Name &&
    //            //u.Email == cmd.Email &&
    //            //u.Role == cmd.Role
    //        ));
    //        await _unitOfWorkMock.Received(1).SaveChangesAsync(Arg.Any<CancellationToken>());
    //    }   
}
