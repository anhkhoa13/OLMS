using OLMS.Domain.Primitives;
using System.Text.RegularExpressions;

using static OLMS.Domain.Error.Error.User;

namespace OLMS.Domain.ValueObjects;

public class Email : ValueObject
{
    private static readonly Regex emailRegex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]{3,100}$", RegexOptions.Compiled);
    public string Value { get; }

    private Email(string value)
    {
        Value = value;
    }

    public static Email Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException(EmptyEmail);

        if (!IsValidEmail(value))
            throw new ArgumentException(InvalidEmail);

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
