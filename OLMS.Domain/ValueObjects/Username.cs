
using OLMS.Domain.Primitives;
using System.Text.RegularExpressions;

using static OLMS.Domain.Error.Error.User;

namespace OLMS.Domain.ValueObjects;

public class Username : ValueObject
{
    private static readonly Regex usernameRegex = new Regex(@"^[a-zA-Z0-9]{3,50}$", RegexOptions.Compiled);
    public string Value { get; }
    private Username(string value)
    {
        Value = value;
    }

    public static Username Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException(EmptyUsername);

        if (!IsValidUsername(value))
            throw new ArgumentException(InvalidUsername);

        return new Username(value);
    }

    private static bool IsValidUsername(string username)
    {
        return usernameRegex.IsMatch(username);
    }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}
