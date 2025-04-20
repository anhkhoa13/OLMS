using OLMS.Domain.Entities.QuestionEntity;
using OLMS.Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OLMS.Domain.Entities.QuizEntity;

public class StudentResponse : Entity
{
    public Guid QuizAttemptId { get; set; }
    public QuizAttempt QuizAttempt { get; set; }
    public Guid QuestionId { get; private set; }
    public Question Question { get; set; }  
    public string ResponseText { get; private set; }
    public bool IsDeleted { get; set; } // Soft delete flag

    public StudentResponse() { }
    public StudentResponse(Guid id, Guid quizAttemptId, Guid questionId, string answer) : base(id)
    {
        QuestionId = questionId;
        QuizAttemptId = quizAttemptId;
        ResponseText = answer;
    }
}
