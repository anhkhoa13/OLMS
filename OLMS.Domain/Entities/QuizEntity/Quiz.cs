using OLMS.Domain.Entities.QuestionEntity;
using OLMS.Domain.Primitives;
using OLMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;

namespace OLMS.Domain.Entities.QuizEntity;

public class Quiz : Entity, IAggregateRoot
{
    public Code Code { get; private set; } = default!;
    public Guid InstructorID { get; set; }
    public string Title { get; protected set; }
    public string? Description { get; protected set; }
    public DateTime StartTime { get; protected set; }
    public DateTime EndTime { get; protected set; }
    public bool IsTimeLimited { get; protected set; }
    public TimeSpan? TimeLimit { get; protected set; }
    public int NumberOfAttempts { get; protected set; }

    private readonly List<Question> _questions = new();
    public IReadOnlyCollection<Question> Questions => _questions.AsReadOnly();


    private readonly List<QuizCourse> _quizCourses = new();
    public IReadOnlyCollection<QuizCourse> QuizCourses => _quizCourses.AsReadOnly();


    private Quiz() : base() { }
    private Quiz(
    Guid id,
    Guid instructorId,
    Code code,
    string title,
    string description,
    DateTime startTime,
    DateTime endTime,
    bool isTimeLimited,
    TimeSpan? timeLimit,
    int numberOfAttempts) : base(id) {
        InstructorID = instructorId;
        Code = code;
        Title = title;
        Description = description;
        StartTime = startTime;
        EndTime = endTime;
        IsTimeLimited = isTimeLimited;
        TimeLimit = timeLimit;
        NumberOfAttempts = numberOfAttempts;
    }

    public static Quiz Create(
        Guid id,
        Guid instructorId,
        string title,
        string description,
        DateTime startTime,
        DateTime endTime,
        bool isTimeLimited,
        TimeSpan? timeLimit,
        int numberOfAttempts = 1
        ) {
        if (string.IsNullOrWhiteSpace(title)) throw new ArgumentNullException(nameof(title), "Title cannot be null");
        if (instructorId == Guid.Empty) throw new ArgumentNullException(nameof(instructorId), "InstructorID cannot be empty");

        if (title.Length < 3 || title.Length > 100)
            throw new ArgumentException("Title must be 3-100 characters long", nameof(title));
        if (description.Length > 100)
            throw new ArgumentException("Description must be less than 100 characters", nameof(description));
        if (startTime >= endTime)
            throw new ArgumentException("Start time must be earlier than end time.", nameof(startTime));
        if (isTimeLimited && (!timeLimit.HasValue || timeLimit.Value.TotalMinutes <= 0))
            throw new ArgumentException("Time limit must be greater than zero when time is limited.", nameof(timeLimit));
        if (numberOfAttempts <= 0)
            throw new ArgumentException("Number of attempts must be at least 1.", nameof(numberOfAttempts));

        var code = Code.Generate(id);
        return new Quiz(
            id,
            instructorId,
            code,
            title,
            description,
            startTime,
            endTime,
            isTimeLimited,
            timeLimit,
            numberOfAttempts
        );
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
    public bool CanStudentAttempt(int previousAttemptCount) {
        return NumberOfAttempts == 0 || previousAttemptCount < NumberOfAttempts;
    }
}
