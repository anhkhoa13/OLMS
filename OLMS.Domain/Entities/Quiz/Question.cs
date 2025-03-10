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
    protected string Content { get; set; }
    protected abstract QuestionType Type { get; }

    protected Question(Guid id, string content) : base(id)
    {
        Content = content;
    }
}
