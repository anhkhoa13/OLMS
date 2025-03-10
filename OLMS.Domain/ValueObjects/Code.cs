
using OLMS.Domain.Primitives;

namespace OLMS.Domain.ValueObjects;

public class Code : ValueObject
{
    public string Value { get; }
    private Code(string value)
    {
        Value = value;
    }

    public static Code Generate(Guid id)
    {
        return new Code(id.ToString("N")[..6].ToUpper());
    }
    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}
