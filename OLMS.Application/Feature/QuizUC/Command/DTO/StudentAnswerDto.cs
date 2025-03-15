using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OLMS.Application.Feature.QuizUC.Command.DTO;

public class StudentAnswerDto
{
    public Guid QuestionId { get; set; }
    public string Answer { get; set; } // Stores "A", "B", "C", "D" for MCQ or "True"/"False" for TrueFalse

    public StudentAnswerDto(Guid questionId, string answer)
    {
        QuestionId = questionId;
        Answer = answer;
    }
}
