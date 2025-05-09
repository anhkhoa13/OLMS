﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OLMS.Presentation.Controllers;
using OLMS.Presentation.Models;
using System.Security.Claims;
using System.Text;

namespace OLMS.Presentation.Areas.Student.Controllers;

[Area("Student")]
[Authorize(Roles = "Student")]
public class HomeController : BaseController
{
    public HomeController(IConfiguration configuration, IHttpClientFactory httpClientFactory) : base(configuration, httpClientFactory)
    {
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
    [HttpPost]
    public async Task<IActionResult> EnrollCourse([FromForm] string code)
    {
        var studentId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
        var request = new
        {
            StudentId = studentId,
            CourseCode = code
        };

        var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
        var apiUrl = "api/student/enroll";
        var response = await _httpClient.PostAsync(apiUrl, content);
        var responseContent = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            ErrorResponseHandler(responseContent);
        }
        
        ViewBag.Message = $"Successfully enroll to course with code {code}";
        return View("Courses");
    }

    public async Task<IActionResult> GetCourses()
    {
        var studentId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
        var apiUrl = $"api/student/courses?studentId={studentId}";

        var response = await _httpClient.GetAsync(apiUrl);
        var responseContent = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            ErrorResponseHandler(responseContent);
            return View("Courses");
        }

        return Ok(responseContent);
    }
}
