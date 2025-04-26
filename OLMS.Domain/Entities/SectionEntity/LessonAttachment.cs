using OLMS.Domain.Primitives;
public class LessonAttachment : Entity
{
    public string Name { get; private set; } = default!;
    public string Type { get; private set; } = default!;
    public byte[] Data { get; private set; } = default!;
    public Guid LessonId { get; private set; }

    // Private constructor for EF Core and factory method usage
    private LessonAttachment() : base(){ }

    private LessonAttachment(Guid id, string name, string type, byte[] data, Guid lessonId) : base(id)
    {
        Name = name;
        Type = type;
        Data = data;
        LessonId = lessonId;
    }

    public static LessonAttachment Create(string name, byte[] data, Guid lessonId)
    {
        // Validate inputs
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name cannot be empty", nameof(name));

        if (data == null || data.Length == 0)
            throw new ArgumentException("Data cannot be empty", nameof(data));

        if (lessonId == Guid.Empty)
            throw new ArgumentException("Exercise ID cannot be empty", nameof(lessonId));

        // Determine file type from name
        string type = Path.GetExtension(name).TrimStart('.');

        // Create new instance
        return new LessonAttachment(Guid.NewGuid(), name, type, data, lessonId);
    }
}
