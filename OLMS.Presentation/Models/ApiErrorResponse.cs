namespace OLMS.Presentation.Models;

public class ApiErrorResponse
{
    public int Code { get; set; }
    public string? Message { get; set; }
    public string? Errors{ get; set; }
}
