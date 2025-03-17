namespace OLMS.Presentation.Models;

public record class ApiErrorResponse(int Code, string Message, string Errors)
{
}
