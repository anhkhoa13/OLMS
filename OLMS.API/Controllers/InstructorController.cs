using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace OLMS.API.Controllers;

[Authorize(Roles = "Instructor")]
public class InstructorController : Controller
{
    private readonly ISender _sender;
    public InstructorController(ISender sender)
    {
        _sender = sender;
    }
}
