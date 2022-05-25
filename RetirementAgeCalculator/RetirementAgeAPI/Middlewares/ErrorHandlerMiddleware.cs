using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using RetirementAgeAPI.Models;
using BusinessLayer.Exceptions;
using Microsoft.Data.SqlClient;

namespace RetirementAgeAPI.Middlewares
{
    public class ErrorHandlerMiddleware 
    {
        private readonly ILogger _logger;
        private readonly RequestDelegate _next;

        public ErrorHandlerMiddleware(RequestDelegate next, ILogger<ErrorHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);

                var message = CreateMessage(context, ex);
                _logger.LogError(message, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception e)
        {
            var result = new ResultDto() { IsSuccessful = false, Message = e.Message };
            int statusCode;

            //if(e is SqlException)
            //{
            //    statusCode = StatusCodes.
            //}
            if (e is ArgumentException || e is ArgumentNullException)
            {
                statusCode = StatusCodes.Status400BadRequest;
            }
            else if (e is BaseException)
            {
                statusCode = StatusCodes.Status422UnprocessableEntity;
            }
            else
            {
                statusCode = StatusCodes.Status500InternalServerError;
                result.Message = "Unknown error.";
            }

            var response = JsonConvert.SerializeObject(result);

            context.Response.StatusCode = statusCode;
            await context.Response.WriteAsync(response);
        }

        private string CreateMessage(HttpContext context, Exception e)
        {
            var message = $"Exception caught in global error handler, exception message:" +
                                    $"{e.Message}, exception stack: {e.StackTrace}";

            if (e.InnerException != null)
            {
                message = $"{message}, inner exception message {e.InnerException.Message}, " +
                                    $"inner exception stack {e.InnerException.StackTrace}";
            }

            return $"{message} RequestId: {context.TraceIdentifier}";
        }
    }    
}
