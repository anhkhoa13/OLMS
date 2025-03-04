using OLMS.Domain.Primitives;
using System.Text.RegularExpressions;

using static OLMS.Domain.Error.Error.User;

namespace OLMS.Domain.ValueObjects;

public sealed class Password : ValueObject
{
    private static readonly Regex PasswordRegex = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,32}$", RegexOptions.Compiled);
    public string Value { get; }
    private Password(string value)
    {
        Value = value;
    }

    public static Password Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException(EmptyPassword);

        if (!PasswordRegex.IsMatch(value))
            throw new ArgumentException(InvalidPassword);

        return new Password(value);
    }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}
