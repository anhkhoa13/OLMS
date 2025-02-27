namespace OLMS.Domain.Primitives;

public abstract class ValueObject : IEquatable<ValueObject>
{
	#region Methods
	public abstract IEnumerable<object> GetAtomicValues();
    public bool Equals(ValueObject? other)
    {
        return other is not null && ValueAreEqual(other);
    }
    public override bool Equals(object? obj)
    {
        return obj is ValueObject other && ValueAreEqual(other);
    }
    public override int GetHashCode()
    {
        return GetAtomicValues()
            .Select(x => x != null ? x.GetHashCode() : 0)
            .Aggregate((x, y) => x ^ y);
    }
    private bool ValueAreEqual(ValueObject other)
    {
        return GetAtomicValues().SequenceEqual(other.GetAtomicValues());
    }
    #endregion
}
