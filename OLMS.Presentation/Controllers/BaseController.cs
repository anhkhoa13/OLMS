using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using OLMS.Shared.DTO;

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

    public void ErrorResponseHandler(string responseContent)
    {
        var error = JsonConvert.DeserializeObject<ErrorResponse>(responseContent);

        if (error?.Errors is JObject errorsObject)
        {
            var errorDict = errorsObject.ToObject<Dictionary<string, string[]>>();
            if (errorDict != null)
            {
                foreach (var key in errorDict.Keys)
                    foreach (var message in errorDict[key])
                        ModelState.AddModelError(key, message);
            }
        }
        else if (error?.Errors is JArray errorArray)
            foreach (var message in errorArray)
            {
                ModelState.AddModelError("", message.ToString());
            }
        else if (error?.Errors is string errorMessage) ModelState.AddModelError("", errorMessage);
        else ModelState.AddModelError("", error?.Message ?? "An unknown error occurred.");
    }
}
