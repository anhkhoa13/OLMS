using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace OLMS.Presentation.Controllers;

[Authorize(Roles = "Instructor")]
public class InstructorController : Controller
{

    public IActionResult Dashboard()
    {
        return View();
    }
}
