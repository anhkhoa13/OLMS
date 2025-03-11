using MediatR;
using Microsoft.AspNetCore.Mvc;
using OLMS.Application.Feature.User;
using OLMS.Domain.Result;

namespace OLMS.Presentation.Controllers;

public class AuthenticationController : Controller
{
    private readonly ILogger<AuthenticationController> _logger;
    private readonly ISender _sender;

    public AuthenticationController(ILogger<AuthenticationController> logger, ISender sender)
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

    [HttpPost]
    public async Task<IActionResult> Register([FromForm]CreateUserCommand command)
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
    public async IActionResult Login([FromForm]LoginUserCommand command)
    {
        Result<string> result = await _sender.Send(command);

        if(!result.IsSuccess)
        {
            ModelState.AddModelError("", result.Error.ErrorMessage ?? "Error");
            return View(command);
        }


        return View();
    }
}
    