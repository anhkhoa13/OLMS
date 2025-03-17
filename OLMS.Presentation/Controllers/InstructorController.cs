using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace OLMS.Presentation.Controllers;

[Authorize(Roles = "Instructor")]
[Route("ins")]
public class InstructorController : BaseController
{
    public InstructorController(IConfiguration configuration, IHttpClientFactory httpClientFactory) : base(configuration, httpClientFactory)
    {
    }

    public IActionResult Dashboard()
    {
        return View();
    }
}
