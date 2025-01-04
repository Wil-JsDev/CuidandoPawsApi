using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace CuidandoPawsApi.Infrastructure.Api.ExceptionHandling
{
    public class ValidationExceptionHandler : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext, 
            Exception exception, 
            CancellationToken cancellationToken)
        {

            if (exception is not ValidationException validationException)
            {
                return true;
            }

            ProblemDetails problemDetails = new ProblemDetails()
            {
                Title = "An error occurred",
                Status = StatusCodes.Status400BadRequest,
                Detail = "One or more validation errors occurred",
                Type = exception.GetType().Name,
                Instance = $"{httpContext.Request.Method} {httpContext.Request.Path}",
                Extensions = { ["Errors"] = validationException.Errors }
            };

            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = problemDetails.Status.Value;
            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

            return true;
        }
    }
}
