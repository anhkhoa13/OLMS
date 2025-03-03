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
        private ISender _sender;
        
        public HomeController(ILogger<HomeController> logger, ISender _sender)
        {
            _logger = logger;
            _sender = _sender;
        }

        public IActionResult Index(CreateUserCommand createUserCommand)
        {
            var dwa = _sender.Send(createUserCommand);
            return View(dwa);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
