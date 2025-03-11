using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OLMS.Domain.Entities.Quiz;

public class MultipleChoiceQuiz : Quiz
{
    public TimeOnly TimeLimit { get; protected set; }

    private readonly List<MultipleChoiceQuestion> _questions = new();
    public IReadOnlyCollection<MultipleChoiceQuestion> Questions => _questions.AsReadOnly();
    public virtual ICollection<MultipleChoiceQuestion> QuestionsCollection { get; private set; } = new List<MultipleChoiceQuestion>();
    public MultipleChoiceQuiz(Guid id, string title, string description, DateTime startTime, DateTime endTime, bool isTimeLimited) : base(id, title, description, startTime, endTime, isTimeLimited)
    {
    }
    public void AddQuestion(MultipleChoiceQuestion question)
    {
        question.QuizId = this.Id;
        _questions.Add(question);
    }
    public void RemoveQuestion(MultipleChoiceQuestion question)
    {
        question.QuizId = this.Id;
        _questions.Remove(question);
    }
    public void ClearQuestions()
    {
        _questions.Clear();
    }
}
