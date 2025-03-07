namespace OLMS.Domain.Primitives;

public abstract class Entity : IEquatable<Entity>
{
    #region Constructors
    protected Entity() { }
    protected Entity(Guid id)
    {
        Id = id;
    }
    #endregion

    #region Methods
    public Guid Id { get; private init; }
    public bool Equals(Entity? other)
    {
        if (other is null)
        {
            return false;
        }
        if (GetType() != other.GetType())
        {
            return false;
        }
        return Id == other.Id;
    }
    public override bool Equals(object? obj)
    {
        if (obj is null)
        {
            return false;
        }
        if (obj.GetType() != GetType())
        {
            return false;
        }
        if (obj is not Entity other)
        {
            return false;
        }

        return Id == other.Id;
    }
    public override int GetHashCode()
    {
        return Id.GetHashCode() * 13;
    }
    #endregion

    #region Static Methods
    public static bool operator ==(Entity? first, Entity? second)
    {
        return first is not null && second is not null &&  first.Equals(second);
    }
    public static bool operator !=(Entity? first, Entity? second)
    {
        return !(first == second);
    }
    #endregion
}
