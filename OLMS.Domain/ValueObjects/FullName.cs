using OLMS.Domain.Primitives;

using static OLMS.Domain.Error.Error.User;

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
            throw new ArgumentException(EmptyName);
        }
        if (value.Length > Length)
        {
            throw new ArgumentException(InvalidFullName);
        }
        return new FullName(value);
    }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}
