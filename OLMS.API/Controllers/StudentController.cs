using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace OLMS.API.Controllers;

[ApiController]
[Route("api/stu")]
public class StudentController : Controller
{
    [HttpGet]
    [Authorize]
    public IActionResult GetInfo()
    {
        return Ok(new { Message = "Student info", User = User.Identity!.Name });
    }
}
