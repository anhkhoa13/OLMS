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
            var error = JsonConvert.DeserializeObject<ErrorResponse>(responseContent);

            if (error?.Errors is JObject errorsObject)
            {
                // Nếu `Errors` là một dictionary, thêm vào ModelState
                var errorDict = errorsObject.ToObject<Dictionary<string, string[]>>();
                if (errorDict != null)
                {
                    foreach (var key in errorDict.Keys)
                    {
                        foreach (var message in errorDict[key])
                        {
                            ModelState.AddModelError(key, message);
                        }
                    }
                }
            }
            else if (error?.Errors is JArray errorArray)
            {
                // Nếu `Errors` là một danh sách lỗi
                foreach (var message in errorArray)
                {
                    ModelState.AddModelError("", message.ToString());
                }
            }
            else if (error?.Errors is string errorMessage)
            {
                // Nếu `Errors` là một chuỗi lỗi đơn
                ModelState.AddModelError("", errorMessage);
            }
            else
            {
                // Nếu không có `Errors`, chỉ hiển thị `Message`
                ModelState.AddModelError("", error?.Message ?? "An unknown error occurred.");
            }
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
            var error = JsonConvert.DeserializeObject<ApiErrorResponse>(responseContent)!;
            ModelState.AddModelError("", error.Message ?? "Error");
            return View("Dashboard");
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
