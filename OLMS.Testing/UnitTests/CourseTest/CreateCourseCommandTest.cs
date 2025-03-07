using FluentAssertions;
using NSubstitute;
using OLMS.Application.Feature.CourseUC;
using OLMS.Application.Feature.User;
using OLMS.Domain.Entities;
using OLMS.Domain.Repositories;
using OLMS.Domain.ValueObjects;
using Xunit;
  
namespace OLMS.Testing.UnitTests.CourseTest;
public class CreateCourseCommandTest {
    private static readonly CreateCourseCommand CMD = new(new Guid(), "Operating System", "Hard", new Guid());

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
        var fullName = FullName.Create("John Doe");
        var userName = Username.Create("JohnDoe");
        var password = Password.Create("Password123!");
        var email = Email.Create("johnDoe@gmail.com");
        var instructor = new Instructor(Guid.NewGuid(), userName, password, fullName, email, 18);

        _userRepository.GetByIdAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>())
            .Returns(instructor);
         
        // Act
        var result = await _handler.Handle(new CreateCourseCommand(Guid.NewGuid(), "Operating System", "Hard", instructor.Id), CancellationToken.None);

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
