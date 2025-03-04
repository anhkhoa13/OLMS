using FluentAssertions;
using NSubstitute;
using OLMS.Application.Feature.CourseUC;
using OLMS.Application.Feature.User;
using OLMS.Domain.Entities;
using OLMS.Domain.Repositories;
using Xunit;

namespace OLMS.Testing.UnitTests.CourseTest;
public class CreateCourseCommandTest {
    private static readonly CreateCourseCommand CMD = new(new Guid(), "OS", "Hard", new Guid());

    private readonly IUnitOfWork _unitOfWork;
    private readonly ICourseRepository _courseRepository;
    private readonly IUserRepository _userRepository;
    private readonly CreateCourseCommandHandler _handler;

    public CreateCourseCommandTest() {
        _unitOfWork = Substitute.For<IUnitOfWork>();
        _courseRepository = Substitute.For<ICourseRepository>();
        _userRepository = Substitute.For<IUserRepository>();
        _handler = new CreateCourseCommandHandler(_unitOfWork, _courseRepository, _userRepository);
    }
    [Fact]
    public async Task Handle_Should_Create_Course_When_Instructor_Is_Valid() {
        // Arrange
        var instructor = new Instructor(Guid.NewGuid(), "John Doe", "john@email.com", "password123");

        _userRepository.GetByIdAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>())
            .Returns(instructor);

        // Act
        var result = await _handler.Handle(CMD, CancellationToken.None);

        // Assert
        result.Should().NotBeEmpty();
        await _courseRepository.Received(1).AddAsync(Arg.Any<Course>());
        // Optional assertion to verify UnitOfWork was called
        await _unitOfWork.Received(1).SaveChangesAsync(Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task Handle_Should_Throw_Exception_When_Instructor_Is_Invalid() {
        // Arrange
        var command = new CreateCourseCommand(Guid.NewGuid(), "Test Course", "Test Description", Guid.NewGuid());
        _userRepository.GetByIdAsync(command.InstructorId, Arg.Any<CancellationToken>()).Returns(Task.FromResult<UserBase>(null));

        // Act
        Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<Exception>().WithMessage("Invalid instructor.");
        await _courseRepository.DidNotReceive().AddAsync(Arg.Any<Course>());
    }
}
