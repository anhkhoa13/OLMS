using OLMS.Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OLMS.Domain.Entities.Quiz;
public enum QuestionType
{
    MultipleChoice,
    TrueFalse,
    ShortAnswer
}
public abstract class Question : Entity
{
    public string Content { get; set; }
    public abstract QuestionType Type { get; protected set; }
    public Guid QuizId { get; set; }

    protected Question(Guid id, string content, Guid quizId) : base(id)
    {
        Content = content;
        QuizId = quizId;
    }
}
