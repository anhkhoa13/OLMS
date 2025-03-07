using OLMS.Domain.Primitives;
using OLMS.Domain.ValueObjects;

namespace OLMS.Domain.Entities;

public abstract class UserBase : Entity
{
    public Username Username { get; private set; } = default!;
    public FullName FullName { get; private set; } = default!;
    public Email Email { get; private set; } = default!;
    public Password Password { get; private set; } = default!;
    public int Age { get; private set; }
    public Role Role { get; private set; }
    protected UserBase() { }
    protected UserBase(Guid id, Username username, Password password, FullName fullname, Email email, int age, Role role) : base(id)
    {
        Username = username;
        Password = password;
        FullName = fullname;
        Email = email;
        Age = age;
        Role = role;
    }

    public static UserBase CreateUser(Guid id, Username username, Password password, FullName fullname, Email email, int age, Role role)
    {
        if (id == Guid.Empty) throw new ArgumentNullException(nameof(id), "Guid cannot be empty");
        if (age <= 0) throw new ArgumentException("Age must greater than 0", nameof(age));

        return role switch
        {
            Role.Student => new Student(id, username, password, fullname, email, age),
            Role.Instructor => new Instructor(id, username, password, fullname, email, age),
            _ => throw new InvalidOperationException("Invalid role")
        };
    }
}
