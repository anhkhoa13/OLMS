using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace OLMS.Presentation.Areas.Instructor.Controllers;

[Area("Instructor")]
[Authorize(Roles = "Instructor")]
public class CourseDetailController : Controller
{
    [HttpGet]
    public IActionResult Index(string courseCode)
    {
        return View();
    }
    public IActionResult AddQuestion()
    {
        return View();
    }

    public IActionResult AddQuizTitle()
    {
        return View();
    }
}
