using CuidandoPawsApi.Infrastructure.Api.ExceptionHandling;

namespace CuidandoPawsApi.Infrastructure.Api.Extensions
{
    public static class AddExceptionHandler
    {
        public static void AddException(this IServiceCollection services)
        {
            services.AddExceptionHandler<DbUpdateExceptionHandler>();
            services.AddExceptionHandler<ValidationExceptionHandler>();
            services.AddExceptionHandler<GlobalExceptionHandler>();
        }
    }
}
