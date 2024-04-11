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

            // log request headers
            //LogHeaders(context.Request.Headers);

            //
            /* log request body (if it's a readable stream and not too large)
            if (context.Request.Body.CanRead && context.Request.ContentLength < 1024)
            {
                context.Request.EnableBuffering(); // Enable request body buffering
                var requestBody = await new StreamReader(context.Request.Body, Encoding.UTF8).ReadToEndAsync();
                _logger.LogInformation($"--------Request Body: {requestBody}");
                context.Request.Body.Position = 0; // Reset stream position for further processing
            }*/

            // Call the next middleware in the pipeline
            await _next(context);

            // Log the response status
            _logger.LogInformation($"-----------Path {context.Request.Path} return with Response Status: {context.Response.StatusCode}");

            // Optionally log response headers
            //LogHeaders(context.Response.Headers);
        }


        private void LogHeaders(IHeaderDictionary headers)
        {
            foreach (var (key, value) in headers)
            {
                _logger.LogInformation($"-------------Header: {key}: {value}");
            }
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
