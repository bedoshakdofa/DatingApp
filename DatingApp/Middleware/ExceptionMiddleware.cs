using DatingApp.Errors;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;

namespace DatingApp.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IHostEnvironment _env;
        private readonly RequestDelegate _next;
        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
        {
            _env = env;
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
     
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                var responese = _env.IsDevelopment()
                     ? new ApiException(context.Response.StatusCode, ex.Message, ex.StackTrace?.ToString()) :
                        new ApiException(context.Response.StatusCode, ex.Message, "internal server Error");

                var option = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
                var json = JsonSerializer.Serialize(responese,option);
                await context.Response.WriteAsync(json);
            }
        }
    }
}
