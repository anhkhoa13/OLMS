using MediatR;
using Microsoft.AspNetCore.Mvc;
using OLMS.Application.Feature.User;
using OLMS.Domain.Result;

namespace OLMS.Presentation.Controllers;

public class AuthenticationController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ISender _sender;

    public AuthenticationController(ILogger<HomeController> logger, ISender sender)
    {
        _logger = logger;
        _sender = sender;
    }
    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    public IActionResult PasswordReset()
    {
        return View();
    }
    //[HttpPost]
    //public IActionResult PasswordReset()
    //{
    //    return View();
    //}

    [HttpPost]
    public async Task<IActionResult> Register(CreateUserCommand command)
    {
        Result<Guid> result = await _sender.Send(command);
        
        if (!result.IsSuccess)
        {
            ModelState.AddModelError("", result.Error.ErrorMessage ?? "Error");
            return View(command); 
        }
        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult Login()
    {
        return View();
    }
}
