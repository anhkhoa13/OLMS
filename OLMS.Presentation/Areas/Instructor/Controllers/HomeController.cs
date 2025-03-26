using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OLMS.Presentation.Controllers;
using System.Security.Claims;
using System.Text;
using OLMS.Presentation.Areas.Instructor.Models;
using OLMS.Presentation.Models;
using Microsoft.AspNetCore.Authorization;
using OLMS.Shared.DTO;
using Newtonsoft.Json.Linq;

namespace OLMS.Presentation.Areas.Instructor.Controllers;

[Area("Instructor")]
[Authorize(Roles = "Instructor")]
public class HomeController : BaseController
{
    public HomeController(IConfiguration configuration, IHttpClientFactory httpClientFactory) : base(configuration, httpClientFactory)
    {
    }

    [HttpPost]
    public async Task<IActionResult> CreateCourse([FromForm] CourseCreateInput input)
    {
        var InstructorId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
        var request = new
        {
            input.Title,
            input.Description,
            InstructorId
        };

        var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
        var apiUrl = "api/instructor/createcourse";
        var response = await _httpClient.PostAsync(apiUrl, content);
        var responseContent = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            ErrorResponseHandler(responseContent);
        }

        var courseResponse = JsonConvert.DeserializeObject<CourseCreateResponse>(responseContent)!;
        ViewBag.CourseCode = courseResponse.Code;

        return View("Courses");
    }

    public async Task<IActionResult> GetCourses()
    {
        var instructorId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
        var apiUrl = $"api/instructor/courses?instructorId={instructorId}";

        var response = await _httpClient.GetAsync(apiUrl);
        var responseContent = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            ErrorResponseHandler(responseContent);
            return View("Courses");
        }

        return Ok(responseContent);
    }
    [HttpGet]
    public IActionResult Dashboard()
    {
        return View();
    }

    [HttpGet]
    public IActionResult Courses()
    {
        return View();
    }

    [HttpGet]
    public IActionResult Profile()
    {
        return View();
    }
}
