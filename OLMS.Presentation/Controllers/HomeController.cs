using MediatR;
using Microsoft.AspNetCore.Mvc;
using OLMS.Application.Feature.User;
using OLMS.Presentation.Models;
using System.Diagnostics;
namespace OLMS.Presentation.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }
        public IActionResult PasswordReset()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return base.View(new Models.ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
         public IActionResult Course()
        {
            return View();
        }

        // Mocked course list (replace with a database if needed)
        private List<dynamic> GetCourseList()
        {
            return new List<dynamic>
            {
                new { Id = 1, Name = "ASP.NET Core", Description = "Learn ASP.NET Core fundamentals." },
                new { Id = 2, Name = "C# Basics", Description = "Beginner-level introduction to C# programming." },
                new { Id = 3, Name = "Entity Framework", Description = "Guide to database management with EF Core." },
                new { Id = 4, Name = "Blazor", Description = "Modern web development with Blazor framework." },
                new { Id = 5, Name = "Microservices", Description = "Build scalable microservices with .NET." }
            };
        }

        // JSON endpoint to return course list
        [HttpGet]
        public JsonResult GetCourses()
        {
            return Json(GetCourseList());
        }

        // JoinCourse: Fetch course by ID and pass it to the view
        public IActionResult JoinCourse(int id)
        {
            var course = GetCourseList().FirstOrDefault(c => c.Id == id);

            if (course == null)
            {
                return NotFound("Course not found.");
            }

            return View(course); // Pass course to the JoinCourse view
        }
    }
}
