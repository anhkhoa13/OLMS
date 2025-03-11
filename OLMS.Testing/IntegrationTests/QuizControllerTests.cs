/*using Moq;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using OLMS.Presentation.Controllers;
using OLMS.Application.Feature.Quiz.Command;


namespace OLMS.Testing.IntegrationTests;

public class QuizControllerTests
{
    private readonly Mock<ISender> _mockSender;
    private readonly QuizController _controller;

    public QuizControllerTests()
    {
        _mockSender = new Mock<ISender>();
        _controller = new QuizController(_mockSender.Object);
    }

    [Fact]
    *//*public async Task CreateQuiz_Should_Return_QuizId()
    {
        // Arrange
        var command = new CreateMulChoiceQuizCommand("Test Quiz", DateTime.Now, DateTime.Now.AddHours(1), true);
        var expectedId = Guid.NewGuid();

        _mockSender
            .Setup(sender => sender.Send(It.IsAny<CreateMulChoiceQuizCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedId);

        // Act
        var result = await _controller.CreateQuiz(command) as OkObjectResult;

        // Assert
        result.Should().NotBeNull();
        result.StatusCode.Should().Be(200);
        var quizId = result.Value.GetType().GetProperty("QuizId")?.GetValue(result.Value, null);
        quizId.Should().Be(expectedId);
    }*//*

    [Fact]
    public async Task RemoveQuestion_Should_Return_NotFound_When_QuestionNotExists()
    {
        // Arrange
        var command = new RemoveMulChQuesCommand(Guid.NewGuid(), Guid.NewGuid());

        _mockSender
            .Setup(sender => sender.Send(It.IsAny<RemoveMulChQuesCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        // Act
        var result = await _controller.RemoveQuestion(command) as NotFoundObjectResult;

        // Assert
        result.Should().NotBeNull();
        result.StatusCode.Should().Be(404);
        result.Value.Should().Be("Question not found");
    }
}
*/