using OLMS.Domain.Entities.CourseAggregate;
using OLMS.Domain.ValueObjects;

namespace OLMS.Domain.Entities;

public class Admin : UserBase
{
    private Admin() : base(){ }
    public Admin(Guid id, Username username, Password password, FullName fullname, Email email, int age) : base(id, username, password, fullname, email, age, Role.Admin)
    {
    }

    public void ApproveCourse(Course course)
    {
        course.Approve();
    }
}
