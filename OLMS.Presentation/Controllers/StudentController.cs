using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace OLMS.Presentation.Controllers;

public class StudentController : Controller
{
    [Authorize(Roles = "Student")]
    public IActionResult Index()
    {
        return View();
    }
}
