using Microsoft.AspNetCore.Mvc;

namespace OLMS.Presentation.Controllers
{
    public class TeacherController : Controller
    {
        public IActionResult Dashboard()
        {
            return View();
        }
    }
}
