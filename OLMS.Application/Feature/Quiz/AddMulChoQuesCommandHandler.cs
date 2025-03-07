﻿using MediatR;
using OLMS.Application.Feature.CourseUC;
using OLMS.Domain.Entities;
using OLMS.Domain.Entities.Quiz;
using OLMS.Domain.Repositories;

namespace OLMS.Application.Feature.Quiz;

public class AddMulChoQuesCommandHandler : IRequestHandler<AddMulChoQuesCommand, Guid>
{
    private readonly IQuizRepository _quizRepo;
    private readonly IQuestionRepository _quesRepo;
    public AddMulChoQuesCommandHandler(IQuestionRepository quesRepo, IQuizRepository quizRepo)
    {
        _quizRepo = quizRepo;
        _quesRepo = quesRepo;
    }
    public async Task<Guid> Handle(AddMulChoQuesCommand request, CancellationToken cancellationToken)
    {
        var quiz = await _quizRepo.GetByIdAsync(request.QuizId);
        if (quiz is not MultipleChoiceQuiz multipleChoiceQuiz)
        {
            throw new InvalidOperationException("Quiz type is not Multiple Choice Quiz");
        }
        var question = new MultipleChoiceQuestion(Guid.NewGuid(), request.Content, request.Options, request.CorrectOptionIndex);
        multipleChoiceQuiz.AddQuestion(question);

        await _quesRepo.AddAsync(question, cancellationToken);
        _quizRepo.Update(quiz);
        await _quesRepo.SaveChangesAsync();

        return question.Id;
    }
}
