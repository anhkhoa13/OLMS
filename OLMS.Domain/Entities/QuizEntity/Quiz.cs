using OLMS.Domain.Primitives;
using System;
using System.Collections.Generic;

namespace OLMS.Domain.Entities.QuizEntity;

public class Quiz : Entity, IAggregateRoot
{
    public string Title { get; protected set; }
    public string? Description { get; protected set; }
    public DateTime StartTime { get; protected set; }
    public DateTime EndTime { get; protected set; }
    public bool IsTimeLimited { get; protected set; }

    private readonly List<Question> _questions = new();
    public IReadOnlyCollection<Question> Questions => _questions.AsReadOnly();

    public Quiz(Guid id, string title, string description, DateTime startTime, DateTime endTime, bool isTimeLimited) : base(id)
    {
        Title = title;
        StartTime = startTime;
        EndTime = endTime;
        IsTimeLimited = isTimeLimited;
        Description = description;
    }


    public void AddQuestion(Question question)
    {
        question.QuizId = this.Id;
        _questions.Add(question);
    }
    public void RemoveQuestion(Question question)
    {
        question.QuizId = this.Id;
        _questions.Remove(question);
    }
    public void ClearQuestions()
    {
        _questions.Clear();
    }
}
