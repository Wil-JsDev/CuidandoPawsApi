using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CuidandoPawsApi.Infrastructure.Api.ExceptionHandling
{
    public class DbUpdateExceptionHandler : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext, 
            Exception exception, 
            CancellationToken cancellationToken)
        {

            if (exception is not DbUpdateException dbUpdateException)
            {
                return true;
            }

            ProblemDetails problemDetails = new ProblemDetails()
            {
                Title = "An error occurred",
                Status = StatusCodes.Status400BadRequest,   
                Detail = "The record already exists with the same ID. Make sure the ID value is unique.",
                Type = exception.GetType().Name,
                Instance = $"{httpContext.Request.Method} {httpContext.Request.Path}",
                Extensions = { ["errors"] = dbUpdateException.Message }
            };

            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = problemDetails.Status.Value;
            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

            return true;
        }
    }
}
