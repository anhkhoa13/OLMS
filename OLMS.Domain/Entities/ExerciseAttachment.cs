using OLMS.Domain.Primitives;
public class ExerciseAttachment : Entity {
    public string Name { get; private set; } = default!;
    public string Type { get; private set; } = default!;
    public byte[] Data { get; private set; } = default!;
    public Guid ExerciseId { get; private set; }

    // Private constructor for EF Core and factory method usage
    private ExerciseAttachment() : base() { }

    private ExerciseAttachment(Guid id, string name, string type, byte[] data, Guid exerciseId) : base(id) {
        Name = name;
        Type = type;
        Data = data;
        ExerciseId = exerciseId;
    }

    public static ExerciseAttachment Create(string name, byte[] data, Guid exerciseId) {
        // Validate inputs
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name cannot be empty", nameof(name));

        if (data == null || data.Length == 0)
            throw new ArgumentException("Data cannot be empty", nameof(data));

        if (exerciseId == Guid.Empty)
            throw new ArgumentException("Exercise ID cannot be empty", nameof(exerciseId));

        // Determine file type from name
        string type = Path.GetExtension(name).TrimStart('.');

        // Create new instance
        return new ExerciseAttachment(Guid.NewGuid(), name, type, data, exerciseId);
    }
}
