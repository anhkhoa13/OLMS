using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OLMS.Domain.Entities.Quiz;

public class MultipleChoiceQuiz : Quiz
{

    public List<MultipleChoiceQuestion> Questions = new();
    public TimeOnly TimeLimit { get; protected set; }

    public MultipleChoiceQuiz(Guid id, string title, DateTime startTime, DateTime endTime, bool isTimeLimited) : base(id, title, startTime, endTime, isTimeLimited)
    {
    }
    public void AddQuestion(MultipleChoiceQuestion question)
    {
        Questions.Add(question);
    }
    public void RemoveQuestion(MultipleChoiceQuestion question)
    {
        Questions.Remove(question);
    }
}
