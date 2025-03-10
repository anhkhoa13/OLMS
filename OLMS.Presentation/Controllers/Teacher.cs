using Microsoft.AspNetCore.Mvc;

namespace OLMS.Presentation.Controllers
{
    public class Teacher : Controller
    {
        public IActionResult Dashboard()
        {
            return View();
        }
    }
}
