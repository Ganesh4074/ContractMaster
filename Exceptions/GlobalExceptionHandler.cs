using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace ContractMaster.Exceptions;

public class GlobadExceptionHandler(ILogger<GlobadExceptionHandler> logger) : IExceptionHandler
{
    private readonly ILogger<GlobadExceptionHandler> _logger=logger;
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        _logger.LogError(exception, "An unhandled Exception");

        var problemDetails=new ProblemDetails
        {
            Status=StatusCodes.Status500InternalServerError,
            Title="Internal Server Error",
            Detail="An error has occured"
        };
        httpContext.Response.StatusCode=StatusCodes.Status500InternalServerError;
        await httpContext.Response.WriteAsJsonAsync(problemDetails,cancellationToken);
        return true;
    }

}