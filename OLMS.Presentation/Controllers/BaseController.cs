using Microsoft.AspNetCore.Mvc;

namespace OLMS.Presentation.Controllers;

public abstract class BaseController : Controller
{
    protected readonly IConfiguration _configuration;
    protected readonly HttpClient _httpClient;
    public BaseController(IConfiguration configuration, IHttpClientFactory httpClientFactory)
    {
        _configuration = configuration;
        _httpClient = httpClientFactory.CreateClient("ApiClient");
    }
}
