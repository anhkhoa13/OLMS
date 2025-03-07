using OLMS.Domain.Primitives;

namespace OLMS.Domain.ValueObjects;

public sealed class FullName : ValueObject
{
    private const int Length = 100;
    public string Value { get; }
    private FullName(string value)
    {
        Value = value;
    }

    public static FullName Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentNullException(nameof(value), "Full name cannot be null or empty");
        }
        if (value.Length > Length)
        {
            throw new ArgumentException("FullName must be 3-100 characters long and contain only letters, numbers, and spaces.");
        }
        return new FullName(value);
    }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}
