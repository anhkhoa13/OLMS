
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using OLMS.Application.Feature.User;
using System.Security.Claims;
using System.Text;
using Newtonsoft.Json;
using OLMS.Presentation.Models;
using OLMS.Application.Services;
using OLMS.Domain.Entities;

namespace OLMS.Presentation.Controllers;

public class AuthenticationController : BaseController
{
    private readonly IAuthService _authService;
    public AuthenticationController(IConfiguration configuration, IHttpClientFactory httpClientFactory, IAuthService authService) : base(configuration, httpClientFactory)
    {
        _authService = authService;
    }

    [HttpGet]
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
    public async Task<IActionResult> Register([FromForm]RegisterUserCommand command)
    {
        var registerData = new
        {
            command.Username,
            command.Password,
            command.FullName,
            command.Email,
            command.Age,
            command.Role
        };
        var apiUrl = "/api/auth/register";
        var content = new StringContent(JsonConvert.SerializeObject(registerData), Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync(apiUrl, content);
        var responseContent = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            var error = JsonConvert.DeserializeObject<ApiErrorResponse>(responseContent)!;

            ModelState.AddModelError("", error.Message ?? "Error");
            return View("Index");
        }

        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<IActionResult> Login([FromForm]LoginUserCommand command)
    {
        var loginData = new
        {
            command.Username,
            command.Password
        };
        var apiUrl = "/api/auth/login";
        var content = new StringContent(JsonConvert.SerializeObject(loginData), Encoding.UTF8, "application/json");
        
        var response = await _httpClient.PostAsync(apiUrl, content);

        var responseContent = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            var error = JsonConvert.DeserializeObject<ApiErrorResponse>(responseContent)!;

            ModelState.AddModelError("", error.Message ?? "Error");
            return View("Index");
        }

        var loginResponse = JsonConvert.DeserializeObject<LoginResponse>(responseContent)!;

        var token = loginResponse.Token!;
        var principal = _authService.DecodeJwt(token);
           
        var userId = principal.FindFirst("nameid")!.Value;
        var fullNameClaim = principal.FindFirst("unique_name")!.Value;
        var roleClaim = principal.FindFirst("role")!.Value;

        await SignInWithCookieAsync(new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, userId),
            new Claim(ClaimTypes.Name, fullNameClaim),
            new Claim("Jwt", loginResponse.Token!),
            new Claim(ClaimTypes.Role, roleClaim!)
        });

        return RedirectToAction("Dashboard", "User");

        //if (!Enum.TryParse(roleClaim, out Role role))
        //{
        //    return RedirectToAction("Index", "Home");
        //}

        //return role switch
        //{
        //    Role.Admin => RedirectToAction("Index", "Admin"),
        //    Role.Student => RedirectToAction("Dashboard", "Student"),
        //    Role.Instructor => RedirectToAction("Dashboard", "Instructor"),
        //    _ => RedirectToAction("Index", "Home")
        //};
    }

    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Index", "Home");
    }
    private async Task SignInWithCookieAsync(IReadOnlyCollection<Claim> claims)
    {
        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var authProperties = new AuthenticationProperties
        {
            IsPersistent = true,
            ExpiresUtc = DateTime.UtcNow.AddHours(2)
        };

        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);
    }
}
    