using OLMS.Domain.Primitives;
using System.Text.RegularExpressions;

namespace OLMS.Domain.ValueObjects;

public class Email : ValueObject
{
    private static readonly Regex emailRegex = new(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$",RegexOptions.Compiled);

    public string Value { get; }

    private Email(string value)
    {
        Value = value;
    }

    public static Email Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentNullException(nameof(value), "Email cannot be null or empty");

        if (!IsValidEmail(value))
            throw new ArgumentException("Invalid email format", nameof(value));

        return new Email(value);
    }

    private static bool IsValidEmail(string email)
    {
        return emailRegex.IsMatch(email);
    }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}
