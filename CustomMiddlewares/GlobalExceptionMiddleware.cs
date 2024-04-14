using Microsoft.AspNetCore.Identity.Data;
using Newtonsoft.Json;
using System.Net;

namespace GBC_Travel_Group_90.CustomMiddlewares
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionMiddleware> _logger;

        public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
        {
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
                await HandleExceptionAsync(context, ex);
            }
        }

        private  Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var response = new
            {
                error = new
                {
                    statusCode = (int)HttpStatusCode.InternalServerError,
                    details = ex.Message,
                    stackTrace = ex.StackTrace
                }
            };
            _logger.LogError(JsonConvert.SerializeObject(response));
            // Store the exception details in TempData

            context.Session.SetString("ExceptionMessage", ex.Message);
            
            context.Response.Redirect("/Error");

            return Task.CompletedTask;
        }
    }
}
