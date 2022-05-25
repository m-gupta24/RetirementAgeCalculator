
using Microsoft.AspNetCore.Builder;
using RetirementAgeAPI.Middlewares;

namespace RetirementAgeAPI.Middlewares
{
    public static class ExceptionHandlerExtension
    {
        public static IApplicationBuilder UseExceptionHandlerMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ErrorHandlerMiddleware>();
        }
    }
}
