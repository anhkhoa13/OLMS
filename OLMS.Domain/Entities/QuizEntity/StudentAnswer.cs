using OLMS.Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OLMS.Domain.Entities.QuizEntity;

public class StudentAnswer : Entity
{
    public Guid QuizAttemptId { get; set; }
    public QuizAttempt QuizAttempt { get; set; }
    public Guid QuestionId { get; private set; }
    public Question Question { get; set; }  
    public string Answer { get; private set; }
    public StudentAnswer(Guid id, Guid quizAttemptId, Guid questionId, string answer) : base(id)
    {
        QuestionId = questionId;
        QuizAttemptId = quizAttemptId;
        Answer = answer;
    }
}
