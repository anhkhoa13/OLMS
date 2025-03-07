﻿using MediatR;

namespace OLMS.Application.Feature.CourseUC;

public record AddMulChoQuesCommand(Guid QuizId,
                                string Content,
                                List<string> Options,
                                int CorrectOptionIndex) : IRequest<Guid>
{
}
