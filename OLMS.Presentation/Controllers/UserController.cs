using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OLMS.Presentation.Models;
using System.Net;
using System.Security.Claims;


namespace OLMS.Presentation.Controllers;

[Route("user")]
[Authorize]
public class UserController : BaseController
{
    public UserController(IConfiguration configuration, IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor) : base(configuration, httpClientFactory)
    {
    }

    [HttpGet("info")]
    public async Task<IActionResult> GetUserInfo()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
        var apiUrl = $"/api/user/info?id={userId}";


        var response = await _httpClient.GetAsync(apiUrl);
        var content = await response.Content.ReadAsStringAsync();

        var responseContent = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                ModelState.AddModelError("", "Unauthorized: Bạn không có quyền truy cập.");
                return View("Info");
            }
            else if (response.StatusCode == HttpStatusCode.Forbidden)
            {
                ModelState.AddModelError("", "Forbidden: Bạn không có quyền thực hiện hành động này.");
                return View("Info");
            }
            var error = JsonConvert.DeserializeObject<ApiErrorResponse>(responseContent);

            ModelState.AddModelError("", error?.Message ?? "Error");
            return View("Info");
        }

        var user = JsonConvert.DeserializeObject<UserInfoResponse>(responseContent)!;

        return View("Info", user);
    }
}
