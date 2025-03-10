using MediatR;
using OLMS.Domain.Entities.Quiz;

public record GetQuizDetailsQuery(Guid QuizId) : IRequest<Quiz>;
