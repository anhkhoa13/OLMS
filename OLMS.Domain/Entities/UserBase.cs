using OLMS.Domain.Primitives;
using OLMS.Domain.ValueObjects;
using static OLMS.Domain.Error.Error.User;

namespace OLMS.Domain.Entities;

public abstract class UserBase : Entity
{
    public Username Username { get; private set; }
    public FullName FullName { get; private set; }
    public Email Email { get; private set; }
    public Password Password { get; private set; }
    public int Age { get; private set; }
    public Role Role { get; private set; }
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
        if(id == Guid.Empty) throw new ArgumentException(EmptyGuid);
        if (age <= 0) throw new ArgumentException(NegativeAge);

        return role switch
        {
            Role.Student => new Student(id, username, password, fullname, email, age),
            Role.Instructor => new Instructor(id, username, password, fullname, email, age),
            _ => throw new InvalidOperationException("Invalid role")
        };
    }
}
