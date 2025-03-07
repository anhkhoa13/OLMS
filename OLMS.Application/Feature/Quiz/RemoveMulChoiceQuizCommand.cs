using MediatR;
using OLMS.Application.Feature.CourseUC;
using OLMS.Domain.Entities.Quiz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OLMS.Application.Feature.Quiz;

public record RemoveMulChoiceQuizCommand(Guid QuizId, Guid QuestionId) : IRequest<bool>
{
}
