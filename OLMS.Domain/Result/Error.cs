namespace OLMS.Domain.Result;

public sealed record Error(string Code, string? ErrorMessage = null)
{
    public static readonly Error None = new(string.Empty);
}

public static class UserError
{   
    public static readonly Error EmptyGuid = new("Empty Guid", "Guid cannot be empty");
    public static readonly Error EmptyUsername = new("Empty Username", "Username cannot be empty");
    public static readonly Error EmptyName = new("Empty Name", "Full name cannot be empty");
    public static readonly Error EmptyEmail = new("Empty Email", "Email cannot be empty");
    public static readonly Error EmptyPassword = new("Empty Password", "Password cannot be empty");

    public static readonly Error InvalidUsername = new("Invalid Username","Username must be 3-50 characters long and contain only letters, numbers, and underscores.");
    public static readonly Error InvalidFullName = new("Invalid FullName", "FullName must be 3-100 characters long and contain only letters, numbers, and spaces.");
    public static readonly Error InvalidEmail = new("Invalid Email", "Invalid email format.");
    public static readonly Error InvalidPassword = new("Invalid Password", "Password must contain at least one uppercase letter, one lowercase letter, one digit, and one special character.");
    public static readonly Error InvalidRole = new("Invalid Role", "Invalid role specified.");

    public static readonly Error NonUniqueUsername = new("Non Unique Username", "Username already exists");
    public static readonly Error NonUniqueEmail = new("Non Unique Email", "Email already exists");

    public static readonly Error NegativeAge = new("Negative Age", "Age must greater than 0");

    public static readonly Error CannotLogin = new("Cannot Login", "Invalid username or password");

    public static readonly Error UserNotFound = new("User Not Found", "User not found");
}

public static class CourseError
{
    public static readonly Error EmptyTitle = new("Empty Title", "Title cannot be empty");
    public static readonly Error EmptyInstructor = new("Empty Instructor", "Instructor cannot be empty");

    public static readonly Error InstructorNotFound = new("Instructor Not Found", "Instructor not found");

    public static readonly Error InvalidTitle = new("Invalid Title", "Title must be 3-100 characters long");
    public static readonly Error InvalidDescription = new("Invalid Description", "Description must be less than 100 characters");

}
