using OLMS.Domain.Entities.InstructorAggregate;
using OLMS.Domain.Entities.QuestionEntity;
using OLMS.Domain.Primitives;
using OLMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;

namespace OLMS.Domain.Entities.QuizEntity;

public class Quiz : Assignment, IAggregateRoot
{
    #region Properties
    public Code Code { get; private set; } = default!;
    public bool IsTimeLimited { get; protected set; }
    public TimeSpan? TimeLimit { get; protected set; }
    #endregion


    private readonly List<Question> _questions = [];
    public IReadOnlyCollection<Question> Questions => _questions.AsReadOnly();


    private readonly List<QuizCourse> _quizCourses = [];
    public IReadOnlyCollection<QuizCourse> QuizCourses => _quizCourses.AsReadOnly();


    private Quiz() : base() { }
    private Quiz(
        Guid id,
        Code code,
        string title,
        string description,
        DateTime startDate,
        DateTime dueDate,
        bool allowLateSubmission,
        int numberOfAttempts,
        bool isTimeLimited,
        TimeSpan? timeLimit,
        Guid sectionId,
        Guid instructorId
        )
        : base(id, title, description, startDate, dueDate, AssignmentType.Quiz, allowLateSubmission, numberOfAttempts, sectionId, instructorId) {
        IsTimeLimited = isTimeLimited;
        TimeLimit = timeLimit;
        Code = code;
    }

    public static Quiz Create(
        string title,
        string description,
        DateTime startDate,
        DateTime dueDate,
        bool allowLateSubmission,
        bool isTimeLimited,
        TimeSpan? timeLimit,
        int numberOfAttempts,
        Guid instructorId,
        Guid sectionId
        ) {
        Validate(title, description, startDate, dueDate, numberOfAttempts, sectionId, instructorId);

        if (isTimeLimited && (!timeLimit.HasValue || timeLimit.Value.TotalMinutes <= 0))
            throw new ArgumentException("Time limit must be greater than zero when time is limited.", nameof(timeLimit));
        
        Guid id = Guid.NewGuid();
        var code = Code.Generate(id);
        return new Quiz(
            id,
            code,
            title,
            description,
            startDate,
            startDate,
            allowLateSubmission,
            numberOfAttempts,
            isTimeLimited,
            timeLimit,
            sectionId,
            instructorId
        );
    }
    public void Update(
    string title,
    string description,
    DateTime startTime,
    DateTime endTime,
    bool allowLateSubmission,
    bool isTimeLimited,
    TimeSpan? timeLimit,
    int numberOfAttempts,
    Guid instructorId,
    Guid sectionId) {
        Title = title;
        Description = description;
        StartDate = startTime;
        DueDate = endTime;
        AllowLateSubmission = allowLateSubmission;
        IsTimeLimited = isTimeLimited;
        TimeLimit = timeLimit;
        NumberOfAttempts = numberOfAttempts;
        InstructorID = instructorId;
        SectionId = sectionId;
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
