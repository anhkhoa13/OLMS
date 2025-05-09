﻿using OLMS.Domain.Entities.QuizEntity;
using OLMS.Domain.Primitives;
using System.Collections.ObjectModel;

public abstract class Assignment : Entity {

    #region Properties
    public string Title { get; set; } = default!;
    public string Description { get; set; } = default!;
    public DateTime StartDate { get; set; }
    public DateTime DueDate { get; set; }
    public AssignmentType Type { get; set; }
    public bool AllowLateSubmission { get; set; }

    public int NumberOfAttempts { get; set; }
    #endregion

    #region Navigations
    public Guid InstructorID { get; set; }
    public Guid SectionId { get; set; }

    //private readonly List<Guid> _assignmentAttempts = [];
    //public IReadOnlyCollection<Guid> AssignmentAttempts => _assignmentAttempts.AsReadOnly();
    #endregion

    protected Assignment() : base(){ }
    protected Assignment(
        Guid id,
        string title,
        string description,
        DateTime startDate,
        DateTime dueDate,
        AssignmentType type,
        bool allowLateSubmission,
        int numberOfAttempts,
        Guid sectionId,
        Guid instructorId
        ) : base(id) {
        Title = title;
        Description = description;
        StartDate = startDate;
        DueDate = dueDate;
        Type = type;
        AllowLateSubmission = allowLateSubmission;
        NumberOfAttempts = numberOfAttempts;
        SectionId = sectionId;
        InstructorID = instructorId;
    } 

    protected static void Validate(
        string title,
        string description,
        DateTime startDate,
        DateTime dueDate,
        int numberOfAttempts,
        Guid sectionId,
        Guid instructorId
        ) {
        if (instructorId == Guid.Empty) throw new ArgumentNullException(nameof(instructorId), "instructorID cannot be empty");
        if (sectionId == Guid.Empty) throw new ArgumentNullException(nameof(sectionId), "sectionId cannot be empty");

        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Title cannot be empty", nameof(title));

        if (title.Length < 3 || title.Length > 100)
            throw new ArgumentException("Title must be 3-100 characters long", nameof(title));

        if (string.IsNullOrWhiteSpace(description))
            throw new ArgumentException("Description cannot be empty", nameof(description));

        if (startDate >= dueDate)
            throw new ArgumentException("Due date must be after start date");

        if (sectionId == Guid.Empty)
            throw new ArgumentException("Section ID cannot be empty", nameof(sectionId));

        if (numberOfAttempts <= 0)
            throw new ArgumentException("Number of attempts must be at least 1.", nameof(numberOfAttempts));
    }
}
