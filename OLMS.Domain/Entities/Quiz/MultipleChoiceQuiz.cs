using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OLMS.Domain.Entities.Quiz;

public class MultipleChoiceQuiz : Quiz
{
    public List<Question> Questions { get; private set; } = new();
    public TimeOnly TimeLimit { get; private set; }

    public MultipleChoiceQuiz(Guid id, string title, DateTime startTime, DateTime endTime, bool isTimeLimited) : base(id, title, startTime, endTime, isTimeLimited)
    {
    }
    public void AddQuestion(Question question)
    {
        Questions.Add(question);
    }
    public void RemoveQuestion(Question question)
    {
        Questions.Remove(question);
    }
}
