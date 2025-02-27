using OLMS.Domain.Primitives;

namespace OLMS.Domain.Entities;

public abstract class UserBase : Entity
{
    public string Name { get; private set; }
    public string Email { get; private set; }
    public string Password { get; private set; }
    public Role Role { get; private set; }
    protected UserBase(Guid id, string name, string email, string password, Role role) : base(id)
    {
        Name = name;
        Email = email;
        Password = password;
        Role = role;
    }

    public static UserBase CreateUser(Guid id, string name, string email, string password, Role role)
    {   
        if(id == Guid.Empty) throw new ArgumentException("Id cannot be empty");
        
        return role switch
        {
            Role.Student => new Student(id, name, email, password),
            Role.Instructor => new Instructor(id, name, email, password),
            _ => throw new InvalidOperationException("Invalid role")
        };
    }
}
