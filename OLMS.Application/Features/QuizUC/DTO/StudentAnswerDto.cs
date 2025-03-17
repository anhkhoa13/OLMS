using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OLMS.Application.Features.QuizUC.DTO;

public class StudentAnswerDto
{
    public Guid QuestionId { get; set; }
    public string Answer { get; set; } 

    public StudentAnswerDto(Guid questionId, string answer)
    {
        QuestionId = questionId;
        Answer = answer;
    }
}
