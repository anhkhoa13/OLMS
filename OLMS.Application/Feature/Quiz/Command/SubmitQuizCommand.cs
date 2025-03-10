using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OLMS.Application.Feature.Quiz.Command;

public record SubmitQuizCommand(Guid AttemptId, List<StudentAnswerDto> Answers) : IRequest<bool>;
public record StudentAnswerDto(Guid QuestionId, string Answer);