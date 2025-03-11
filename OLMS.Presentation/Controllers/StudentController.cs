using Microsoft.AspNetCore.Mvc;

namespace OLMS.Presentation.Controllers
{
    public class StudentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
