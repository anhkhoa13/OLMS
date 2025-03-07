/*using Moq;
using Xunit;
using FluentAssertions;
using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using MediatR;
using OLMS.Application.Feature.Quiz;

namespace OLMS.Testing.IntegrationTests;

public class QuizControllerTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;
    private readonly Mock<ISender> _mockSender;

    public QuizControllerTests(WebApplicationFactory<Program> factory)
    {
        _mockSender = new Mock<ISender>();

        _client = factory.WithWebHostBuilder(builder =>
        {
            builder.ConfigureServices(services =>
            {
                // Replace ISender with Mock version
                services.AddSingleton(_mockSender.Object);
            });
        }).CreateClient();
    }

    [Fact]
    public async Task CreateQuiz_Should_Return_QuizId()
    {
        // Arrange
        var command = new CreateMulChoiceQuizCommand("Test Quiz", DateTime.Now, DateTime.Now.AddHours(1), true);
        var expectedId = Guid.NewGuid();

        _mockSender
            .Setup(sender => sender.Send(It.IsAny<CreateMulChoiceQuizCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedId);

        // Act
        var response = await _client.PostAsJsonAsync("/api/quiz/create", command);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var result = await response.Content.ReadFromJsonAsync<dynamic>();
        ((string)result?.QuizId).Should().Be(expectedId.ToString());
    }

    [Fact]
    public async Task RemoveQuestion_Should_Return_NotFound_When_QuestionNotExists()
    {
        // Arrange
        var command = new RemoveMulChoiceQuizCommand(Guid.NewGuid(), Guid.NewGuid());
        _mockSender
            .Setup(sender => sender.Send(It.IsAny<RemoveMulChoiceQuizCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        // Act
        var response = await _client.DeleteAsJsonAsync("/api/quiz/remove-question", command);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        var result = await response.Content.ReadAsStringAsync();
        result.Should().Contain("Question not found");
    }
}
*/