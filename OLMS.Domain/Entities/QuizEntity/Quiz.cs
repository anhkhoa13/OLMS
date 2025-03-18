using OLMS.Domain.Entities.QuestionEntity;
using OLMS.Domain.Primitives;
using OLMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;

namespace OLMS.Domain.Entities.QuizEntity;

public class Quiz : Entity, IAggregateRoot
{
    public Code Code { get; private set; } = default!;
    public string Title { get; protected set; }
    public string? Description { get; protected set; }
    public DateTime StartTime { get; protected set; }
    public DateTime EndTime { get; protected set; }
    public bool IsTimeLimited { get; protected set; }

    private readonly List<Question> _questions = new();
    public IReadOnlyCollection<Question> Questions => _questions.AsReadOnly();

    private Quiz() : base() { }
    public Quiz(Guid id, Code code, string title, string description, DateTime startTime, DateTime endTime, bool isTimeLimited) : base(id)
    {
        Title = title;
        StartTime = startTime;
        EndTime = endTime;
        IsTimeLimited = isTimeLimited;
        Description = description;
        Code = code;
    }
    public static Quiz Create(
        Guid id, 
        string title, 
        string description, 
        DateTime startTime, 
        DateTime endTime, 
        bool isTimeLimited)
    {
        // logic khi tạo course
        if (string.IsNullOrWhiteSpace(title)) throw new ArgumentNullException(nameof(title), "Title cannot be null");

        if (title.Length < 3 || title.Length > 100) 
            throw new ArgumentException("Title must be 3-100 characters long", nameof(title));
        if (description.Length > 100) 
            throw new ArgumentException("Description must be less than 100 characters", nameof(description));
        if (startTime >= endTime)
            throw new ArgumentException("Start time must be earlier than end time.", nameof(startTime));

        var code = Code.Generate(id);
        return new Quiz(id, code, title, description, startTime, endTime, isTimeLimited);
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
}
