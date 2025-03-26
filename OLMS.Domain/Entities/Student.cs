using OLMS.Domain.Primitives;
using OLMS.Domain.ValueObjects;

namespace OLMS.Domain.Entities;

public class Student : UserBase
{
    public string? Major { get; set; }
    public DateTime? EnrollmentDate { get; set; }

    private Student() : base() { }
    public Student(Guid id, Username username, Password password, FullName fullname, Email email, int age) : base(id, username, password, fullname, email, age, Role.Student)
    {
    }
    
}
