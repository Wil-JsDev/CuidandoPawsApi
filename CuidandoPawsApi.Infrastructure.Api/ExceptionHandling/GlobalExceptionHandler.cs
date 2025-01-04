using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace CuidandoPawsApi.Infrastructure.Api.ExceptionHandling
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext, 
            Exception exception, 
            CancellationToken cancellationToken)
        {
            ProblemDetails problemDetails = new ()
            {
                Title = "An error occurred",
                Status = StatusCodes.Status400BadRequest,
                Detail = exception.Message
            };

            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = problemDetails.Status.Value;
            await httpContext.Response.WriteAsJsonAsync(problemDetails,cancellationToken);

            return true;
        }
    }
}
