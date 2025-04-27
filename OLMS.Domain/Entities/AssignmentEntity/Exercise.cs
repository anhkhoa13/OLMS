using OLMS.Domain.ValueObjects;
using System.Collections.ObjectModel;

public class Exercise : Assignment 
{
    #region
    private readonly List<ExerciseAttachment> _exerciseAttachments = [];
    public IReadOnlyCollection<ExerciseAttachment> ExerciseAttachments => _exerciseAttachments.AsReadOnly();
    #endregion

    private Exercise() : base() { } // Required for EF Core

    private Exercise(
        Guid id,
        string title,
        string description,
        DateTime startDate,
        DateTime dueDate,
        bool allowLateSubmission,
        int numberOfAttempts,
        Guid sectionId,
        Guid instructorId
        )
        : base(id, title, description, startDate, dueDate, AssignmentType.Quiz, allowLateSubmission, numberOfAttempts, sectionId, instructorId) {

    }

    public static Exercise Create(
        string title,
        string description,
        DateTime startDate,
        DateTime dueDate,
        int numberOfAttempts,
        bool allowLateSubmission,
        Guid sectionId,
        Guid instructorId
        ) {
        Validate(title, description, startDate, dueDate, numberOfAttempts, sectionId, instructorId);

        return new Exercise(Guid.NewGuid(), title, description, startDate, dueDate, allowLateSubmission, numberOfAttempts, sectionId, instructorId);
    }

    public void AddAttachment(ExerciseAttachment attachment) {
        _exerciseAttachments.Add(attachment);
    }
}
