using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OLMS.Presentation.Models;
using System.Reflection;
using System.Security.Claims;
using System.Text;

namespace OLMS.Presentation.Controllers;

[Authorize(Roles = "Instructor")]
[Route("ins")]
public class InstructorController : BaseController
{
    public InstructorController(IConfiguration configuration, IHttpClientFactory httpClientFactory) : base(configuration, httpClientFactory)
    {
    }

    //[HttpPost]
    //public async Task<IActionResult> CreateCourse([FromForm] CourseCreateInput input)
    //{
    //    var InstructorId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
    //    var request = new
    //    {
    //        input.Title,
    //        input.Description,
    //        InstructorId
    //    };

    //    var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
    //    var apiUrl = "api/instructor/createcourse";
    //    var response = await _httpClient.PostAsync(apiUrl, content);
    //    var responseContent = await response.Content.ReadAsStringAsync();


    //    if (!response.IsSuccessStatusCode)
    //    {
    //        var error = JsonConvert.DeserializeObject<ApiErrorResponse>(responseContent)!;

    //        ModelState.AddModelError("", error.Message ?? "Error");
    //        return View("Dashboard");
    //    }

    //    var courseResponse = JsonConvert.DeserializeObject<CourseCreateResponse>(responseContent)!;
    //    ViewBag.CourseCode = courseResponse.Code;

    //    return View("Dashboard");
    //}


    //[HttpGet("courses")]
    //public async Task<IActionResult> GetCourses()
    //{
    //    var instructorId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
    //    var apiUrl = $"api/instructor/courses?instructorId={instructorId}";

    //    var response = await _httpClient.GetAsync(apiUrl);
    //    var responseContent = await response.Content.ReadAsStringAsync();
    //    if (!response.IsSuccessStatusCode)
    //    {
    //        var error = JsonConvert.DeserializeObject<ApiErrorResponse>(responseContent)!;
    //        ModelState.AddModelError("", error.Message ?? "Error");
    //        return View("Dashboard");
    //    }
        
    //    return Ok(responseContent);
    //}
}
