using Microsoft.AspNetCore.Diagnostics;
using OLMS.Domain.Result;
using OLMS.Shared.DTO;

namespace OLMS.API.Middleware;

public class GlobalExecptionHandler : IExceptionHandler
{
    private readonly ILogger<GlobalExecptionHandler> _logger;
    public GlobalExecptionHandler(ILogger<GlobalExecptionHandler> logger)
    {
        _logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        _logger.LogError(exception, "Exception: {Message}", exception.Message);
        var errorrDetail = new ErrorResponse
        {
            StatusCode = StatusCodes.Status500InternalServerError,
            Message = "Server Error",
            ErrorCode = "INTERNAL_SERVER_ERROR"
        };

        httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
        await httpContext.Response.WriteAsJsonAsync(errorrDetail, cancellationToken);

        return true;
    }
}
