using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace OLMS.API.Controllers;

[ApiController]
[Authorize(Roles = "Student")]
[Route("api/student")]
public class StudentController : Controller
{
    private readonly ISender _sender;
    public StudentController(ISender sender)
    {
        _sender = sender;
    }
}
    
