
using OLMS.Domain.Primitives;

namespace OLMS.Domain.Entities.AssignmentAttempt;
public class SubmitAttachment : Entity
{
    public string Name { get; private set; } = default!;
    public string Type { get; private set; } = default!;
    public byte[] Data { get; private set; } = default!;
    public Guid ExerciseAttemptId { get; private set; }

    private SubmitAttachment() : base() { }
    public SubmitAttachment(Guid id, string name, string type, byte[] data, Guid exerciseAttemptId) : base(id)
    {
        Name = name;
        Type = type;
        Data = data;
        ExerciseAttemptId = exerciseAttemptId;
    }

    public static SubmitAttachment Create(string name, byte[] data, Guid exerciseAttemptId)
    {
        // Validate inputs
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name cannot be empty", nameof(name));

        if (data == null || data.Length == 0)
            throw new ArgumentException("Data cannot be empty", nameof(data));

        if (exerciseAttemptId == Guid.Empty)
            throw new ArgumentException("Exercise ID cannot be empty", nameof(exerciseAttemptId));

        // Determine file type from name
        string type = Path.GetExtension(name).TrimStart('.');

        // Create new instance
        return new SubmitAttachment(Guid.NewGuid(), name, type, data, exerciseAttemptId);
    }
}
