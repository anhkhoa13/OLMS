using MediatR;
using OLMS.Domain.Entities.QuizEntity;

public record GetQuizDetailsQuery(Guid QuizId) : IRequest<Quiz>;
