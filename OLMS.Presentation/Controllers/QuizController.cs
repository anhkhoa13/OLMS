using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace OLMS.Presentation.Controllers;

public class QuizController : Controller
{
    private readonly ISender _sender; 

    public QuizController(ISender sender)
    {
        _sender = sender;
    }
    public IActionResult Create()
    {
        return View();
    }
    public IActionResult AddQuestion()
    {
        return View();
    }
}
