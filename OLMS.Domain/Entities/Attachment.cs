using OLMS.Domain.Primitives;
public class Attachment : Entity {
    public string Name { get; private set; }
    public string Type { get; private set; }
    public byte[] Data { get; private set; }
    public Guid ExerciseId { get; private set; }

    // Private constructor for EF Core and factory method usage
    private Attachment() { }

    private Attachment(string name, string type, byte[] data, Guid exerciseId) {
        Name = name;
        Type = type;
        Data = data;
        ExerciseId = exerciseId;
    }

    public static Attachment Create(string name, byte[] data, Guid exerciseId) {
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
        return new Attachment(name, type, data, exerciseId);
    }
}
