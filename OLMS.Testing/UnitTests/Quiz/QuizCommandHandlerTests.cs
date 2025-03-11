using OLMS.Application.Feature.CourseUC;
using OLMS.Application.Feature.Quiz.Command;
using OLMS.Domain.Entities.Quiz;
using OLMS.Domain.Repositories;
using Xunit;
using Moq;
using FluentAssertions;

namespace OLMS.Testing.UnitTests.Quiz;

public class QuizCommandHandlerTests
{
    private readonly Mock<IQuizRepository> _mockQuizRepo;
    private readonly Mock<IQuestionRepository> _mockQuesRepo;
    private readonly Mock<IUnitOfWork> _mockUnitOfWork;

    public QuizCommandHandlerTests()
    {
        _mockQuizRepo = new Mock<IQuizRepository>();
        _mockQuesRepo = new Mock<IQuestionRepository>();
        _mockUnitOfWork = new Mock<IUnitOfWork>();
    }

    [Fact]
    public async Task CreateMulChoiceQuiz_Should_ReturnQuizId()
    {
        // Arrange
        var command = new CreateMulChoiceQuizCommand("Quiz Title", DateTime.Now, DateTime.Now.AddHours(1), true);
        var handler = new CreateMulChoiceQuizCommandHandler(_mockQuizRepo.Object, _mockUnitOfWork.Object);
        _mockQuizRepo.Setup(repo => repo.AddAsync(It.IsAny<MultipleChoiceQuiz>(), It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);
        _mockUnitOfWork.Setup(u => u.SaveChangesAsync(It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeEmpty();
        _mockQuizRepo.Verify(repo => repo.AddAsync(It.IsAny<MultipleChoiceQuiz>(), It.IsAny<CancellationToken>()), Times.Once);
        _mockUnitOfWork.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task RemoveMulChoQues_Should_ReturnTrue_When_QuestionRemoved()
    {
        // Arrange
        var quizId = Guid.NewGuid();
        var questionId = Guid.NewGuid();
        var quiz = new MultipleChoiceQuiz(quizId, "Quiz Title", DateTime.Now, DateTime.Now.AddHours(1), true);
        var question = new MultipleChoiceQuestion(questionId, "Question Content", new List<string> { "A", "B" }, 0);
        quiz.AddQuestion(question);

        _mockQuizRepo.Setup(repo => repo.GetByIdAsync(quizId, It.IsAny<CancellationToken>())).ReturnsAsync(quiz);
        var handler = new RemoveMulChoQuesCommandHandler(_mockQuizRepo.Object);

        var command = new RemoveMulChQuesCommand(quizId, questionId);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().BeTrue();
        _mockQuizRepo.Verify(repo => repo.Update(quiz), Times.Once);
        _mockQuizRepo.Verify(repo => repo.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task AddMulChoQues_Should_ReturnQuestionId()
    {
        // Arrange
        var quizId = Guid.NewGuid();
        var command = new AddMulChoQuesCommand(quizId, "Question Content", new List<string> { "A", "B" }, 1);
        var quiz = new MultipleChoiceQuiz(quizId, "Quiz Title", DateTime.Now, DateTime.Now.AddHours(1), true);

        _mockQuizRepo.Setup(repo => repo.GetByIdAsync(quizId, It.IsAny<CancellationToken>())).ReturnsAsync(quiz);
        _mockQuesRepo.Setup(repo => repo.AddAsync(It.IsAny<MultipleChoiceQuestion>(), It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);

        var handler = new AddMulChoQuesCommandHandler(_mockQuesRepo.Object, _mockQuizRepo.Object);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeEmpty();
        _mockQuizRepo.Verify(repo => repo.Update(quiz), Times.Once);
        _mockQuesRepo.Verify(repo => repo.AddAsync(It.IsAny<MultipleChoiceQuestion>(), It.IsAny<CancellationToken>()), Times.Once);
        _mockQuesRepo.Verify(repo => repo.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }
}
