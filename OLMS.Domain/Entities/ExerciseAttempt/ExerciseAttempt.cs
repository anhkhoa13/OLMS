
using OLMS.Domain.Primitives;

namespace OLMS.Domain.Entities.AssignmentAttempt;

public class ExerciseAttempt : AggregateRoot
{
    #region Properties
    public float Score { get; private set; }
    public DateTime SubmitAt { get; set; }
    public ExerciseAttemptStatus Status { get; set; }
    #endregion
    #region Navigation
    private readonly List<SubmitAttachment> _submitAttachtment = [];
    public IReadOnlyCollection<SubmitAttachment> SubmitAttachtment => _submitAttachtment.AsReadOnly();
    public Guid ExerciseId { get; private set; }
    public Guid StudentId { get; private set; }
    #endregion

    private ExerciseAttempt() : base() { }
    public ExerciseAttempt(Guid id, DateTime submitAt, Guid exerciseId, Guid studentId) : base(id)
    {
        Score = 0f;
        SubmitAt = submitAt;
        Status = ExerciseAttemptStatus.Unscored;
        ExerciseId = exerciseId;
        StudentId = studentId;
    }
    public void SetScore(float score) {
        Score = score;
        Status = ExerciseAttemptStatus.Scored;
    }

    public void AddAttachment(SubmitAttachment attachment)
    {
        if (attachment == null)
            throw new ArgumentNullException(nameof(attachment));
        _submitAttachtment.Add(attachment);
    }

    public void ScoreExercise(float score)
    {
        if (score < 0 || score > 100)
            throw new ArgumentOutOfRangeException(nameof(score), "Score must be between 0 and 100.");
        Score = score;
        Status = ExerciseAttemptStatus.Scored;
    }
}
