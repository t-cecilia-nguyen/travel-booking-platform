using GBC_Travel_Group_90.Areas.TravelManagement.Models;
using GBC_Travel_Group_90.CustomMiddlewares;
using GBC_Travel_Group_90.Services;
using Microsoft.AspNetCore.Identity;
using System.Text;

namespace GBC_Travel_Group_90.CustomMiddlewares
{
    public class LoggingMiddleware  //constructed at app startup, therefore has application life time, share between other middleware and types
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<LoggingMiddleware> _logger;
        public LoggingMiddleware(RequestDelegate next, ILogger<LoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {

            //should log incoming request and reponse status

            _logger.LogInformation($"----------Incoming Request: {context.Request.Method} on  Path: {context.Request.Path}");

            
            // Call the next middleware in the pipeline
            await _next(context);

            // Log the response status
            _logger.LogInformation($"----------- Response Status: {context.Response.StatusCode} on Path {context.Request.Path} ");

        }



    }   

    public static class LoggingMiddlewareExtensions
    {
        public static IApplicationBuilder UseLoggingMiddleware(this IApplicationBuilder builder) 
        {
            return builder.UseMiddleware<LoggingMiddleware>();
        }
    }
}
