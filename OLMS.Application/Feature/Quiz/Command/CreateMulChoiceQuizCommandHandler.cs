using MediatR;
using OLMS.Domain.Entities.Quiz;
using OLMS.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OLMS.Application.Feature.Quiz.Command;

public class CreateMulChoiceQuizCommandHandler : IRequestHandler<CreateMulChoiceQuizCommand, Guid>
{
    private readonly IQuizRepository _quizRepo;
    private readonly IUnitOfWork _unitOfWork;

    public CreateMulChoiceQuizCommandHandler(IQuizRepository repository, IUnitOfWork unitOfWork)
    {
        _quizRepo = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> Handle(CreateMulChoiceQuizCommand request, CancellationToken cancellationToken)
    {
        var quiz = new MultipleChoiceQuiz(Guid.NewGuid(), request.Title, request.StartTime, request.EndTime, request.IsTimeLimited);

        /*foreach (var questionCommand in request.Questions)
        {
            var question = new MultipleChoiceQuestion(Guid.NewGuid(),questionCommand.Content, questionCommand.Options, questionCommand.CorrectOptionIndex);
            quiz.AddQuestion(question);
        }*/

        await _quizRepo.AddAsync(quiz);
        await _quizRepo.SaveChangesAsync();
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return quiz.Id;
    }
}
