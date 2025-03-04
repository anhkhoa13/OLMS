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
    public Role Role { get; private set; }
    protected UserBase(Guid id, Username username, Password password, FullName fullname, Email email, Role role) : base(id)
    {
        Username = username;
        Password = password;
        FullName = fullname;
        Email = email;
        Role = role;
    }

    public static UserBase CreateUser(Guid id, Username username, Password password, FullName fullname, Email email, Role role)
    {   
        if(id == Guid.Empty) throw new ArgumentException(EmptyGuid);
        //if(string.IsNullOrWhiteSpace(username)) throw new ArgumentException(EmptyUsername);
        //if (string.IsNullOrWhiteSpace(fullname)) throw new ArgumentException(EmptyName);
        //if (string.IsNullOrWhiteSpace(email)) throw new ArgumentException(EmptyEmail);
        //if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException(EmptyPassword);

        return role switch
        {
            Role.Student => new Student(id, username, password, fullname, email),
            Role.Instructor => new Instructor(id, username, password, fullname, email),
            _ => throw new InvalidOperationException("Invalid role")
        };
    }
}
