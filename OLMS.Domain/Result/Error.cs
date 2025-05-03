using System.Net;

namespace OLMS.Domain.Result;

public sealed record Error(string Code, string? ErrorMessage = null)
{
    public static readonly Error None = new(string.Empty);
    public static Error NotFound(string message) => new("NotFound", message);
    public static Error Validation(string message) => new("Validation", message);
    public static Error Failure(string message) => new("Failure", message);
    public static Error Conflict(string message) => new("Conflict", message);
    public static Error Forbidden(string message) => new("Forbidden", message);
    public static Error Unauthorized(string message) => new("Unauthorized", message);
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

    public static readonly Error CourseNotFound = new("Course Not Found", "Course not found");

    public static readonly Error CourseInactive = new("Course.Inactive", "Course is not active");
    public static readonly Error EnrollmentClosed = new("Course.Enrollment.Closed", "Course enrollment is closed");

}
public static class SectionError {
    public static readonly Error EmptyTitle = new("Section.EmptyTitle", "Section title cannot be empty");
    public static readonly Error InvalidTitleLength = new("Section.InvalidTitleLength",
        "Section title must be between 3 and 100 characters");
    public static readonly Error SectionNotFound = new("Section.NotFound", "Section not found");
    public static readonly Error DuplicateTitle = new("Section.DuplicateTitle",
        "A section with this title already exists in the course");
    public static readonly Error InvalidOrder = new("Section.InvalidOrder",
        "Section order must be a positive integer");
}

public static class ContentError {
    public static readonly Error InvalidContentType = new("Content.InvalidType",
        "Invalid content type specified");
    public static readonly Error FileTooLarge = new("Content.FileTooLarge",
        "File size exceeds maximum allowed limit");
    public static readonly Error UnsupportedFormat = new("Content.UnsupportedFormat",
        "File format is not supported");
}

