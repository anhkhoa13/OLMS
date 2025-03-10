using OLMS.Domain.Primitives;
using System.Text.RegularExpressions;

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
            throw new ArgumentNullException(nameof(value), "Username cannot be null or empty");

        if (!IsValidUsername(value))
            throw new ArgumentException("Username must be 3-50 characters long and contain only letters, and numbers", nameof(value));

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
