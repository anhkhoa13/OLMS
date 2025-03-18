using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace OLMS.Presentation.Controllers;

[Authorize(Roles = "Student")]
[Route("stu")]
public class StudentController : BaseController
{
    public StudentController(IConfiguration configuration, IHttpClientFactory httpClientFactory) : base(configuration, httpClientFactory)
    {
    }
}
