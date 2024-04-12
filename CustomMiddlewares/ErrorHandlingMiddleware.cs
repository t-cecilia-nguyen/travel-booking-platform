using Serilog;
using System.Net;
using System.Reflection;
using Newtonsoft.Json;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;



namespace GBC_Travel_Group_90.CustomMiddlewares
{
/*
    /// <summary>
    /// Error Handler for all exceptions.
    /// </summary>
    public class ErrorHandlingMiddleware
    {
        // Enrich is a custom extension method that enriches the Serilog functionality 
        private static readonly ILogger<ErrorHandlingMiddleware> _logger = (ILogger<ErrorHandlingMiddleware>)Log.ForContext<ErrorHandlingMiddleware>();

        private readonly RequestDelegate _next;

        /// <summary>
        /// Gets HTTP status code response and message to be returned to the caller.
        /// Use the ".Data" property to set the key of the messages if it's localized.
        /// </summary>
        /// <param name="exception">The actual exception</param>
        /// <returns>Tuple of HTTP status code and a message</returns>
        public  (HttpStatusCode code, string message, string statckTrace) GetResponse(Exception exception)
        {
            HttpStatusCode code;
            switch (exception)
            {
                case KeyNotFoundException
                    or NoSuchUserException
                    or FileNotFoundException:
                    code = HttpStatusCode.NotFound;
                    break;
                case EntityAlreadyExists:
                    code = HttpStatusCode.Conflict;
                    break;
                case UnauthorizedAccessException
                    or ExpiredPasswordException
                    or UserBlockedException:
                    code = HttpStatusCode.Unauthorized;
                    break;
                case CreateUserException
                    or ResetPasswordException
                    or ArgumentException
                    or InvalidOperationException:
                    code = HttpStatusCode.BadRequest;
                    break;
                case UnauthorizedAccessException
                    or UserBlockedException:
                    code = HttpStatusCode.Unauthorized;
                    break;
                case BookingValidatorException
                    or InvalidOperationException:
                    code = HttpStatusCode.BadRequest;
                    break;
                default:
                    code = HttpStatusCode.InternalServerError;
                    break;
            }
            return (code, JsonConvert.SerializeObject(new SimpleResponse(exception.Message, exception.StackTrace)));
        }
        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception exception)
            {
                // log the error
                _logger.LogError("-------" + exception, "error during executing {Context}", context.Request.Path.Value);
                var response = context.Response;
                response.ContentType = "application/json";

                // get the response code and message
                var (status, message, stackTrace) = GetResponse(exception);
                response.StatusCode = (int)status;
                await response.WriteAsync(message);
                await response.WriteAsync(stackTrace);

            }
        }
    }


    public sealed class MyCustomException : Exception
    {
        public BookingValidatorException(string message = "An error has Occurred while attempting your booking order.") : base(message, stackTrace)
        {
            Data.Add(ErrorHandlingMiddleware);
        }
        public  NoSuchUserException((string message = "User Not Found") : base(message)
        {

        }
    }
   */





namespace GBC_Travel_Group_90.CustomMiddlewares
    {
        public class ErrorHandlingMiddleware
        {
            private  readonly ILogger<ErrorHandlingMiddleware> _logger ;

            private readonly RequestDelegate _next;

            public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
            {
                _next = next;
                _logger = logger;
            }

            public async Task Invoke(HttpContext context)
            {
                try
                {
                    await _next(context);
                }
                catch (Exception exception)
                {
                    _logger.LogError(exception, "Error during executing {Context}", context.Request.Path);

                    var response = context.Response;
                    response.ContentType = "text/html";

                    // Determine the appropriate status code based on the exception
                    var (code, message, stackTrace, timeStamp) = GetResponse(exception);

                    response.StatusCode = (int)code;
                    int statusCode = response.StatusCode;

                    // Redirect to the ErrorController with the status code
                    // Construct the URL to the desired controller action
                    string url = $"{context.Request.Scheme}://{context.Request.Host}/Error/{statusCode}";

                    // Perform the redirection
                    context.Response.Redirect(url);


                }
            }


            public (HttpStatusCode code, string message, string stackTrace, DateTime timeStamp) GetResponse(Exception exception)
            {
                HttpStatusCode code;
                string message;
                string? stackTrace = exception.StackTrace;
                DateTime timeStamp = DateTime.UtcNow;

                switch (exception)
                {
                    case KeyNotFoundException:
                    case NoSuchUserException:
                    case FileNotFoundException:
                        code = HttpStatusCode.NotFound;
                        message = "Resource not found.";
                        break;
                    case EntityAlreadyExists:
                        code = HttpStatusCode.Conflict;
                        message = "Entity already exists.";
                        break;
                    case UnauthorizedAccessException:
                    case ExpiredPasswordException:
                    case UserBlockedException:
                        code = HttpStatusCode.Unauthorized;
                        message = "Unauthorized access.";
                        break;
                    case CreateUserException:
                    case ResetPasswordException:
                    case ArgumentException:
                    case InvalidOperationException:
                    

                        code = HttpStatusCode.BadRequest;
                        message = "Bad request.";
                        break;
                    case InvalidCastException:
                        code = HttpStatusCode.BadRequest;
                        message = "Cannot cast object type.";
                        break;
                    case BookingValidatorException:
                        code = HttpStatusCode.BadRequest;
                        message = "Booking validation failed.";
                        break;
                    case DbUpdateException:
                        code = HttpStatusCode.BadRequest;
                        message = "Unable to update database.";
                        break;
                    default:
                        code = HttpStatusCode.InternalServerError;
                        message = "Internal server error.";
                        break;
                }

                return (code, message, stackTrace, timeStamp);
            }
        }



        // Define custom exception classes 
        public class BookingValidatorException : Exception
        {
            public BookingValidatorException(string message = "Booking validation failed.", Exception innerException = null) : base(message, innerException)
            {
            }
        }

        public class NoSuchUserException : Exception
        {
            public NoSuchUserException(string message = "User not found.", Exception innerException = null) : base(message, innerException)
            {
            }
        }
        public class EntityAlreadyExists : Exception
        {
            public EntityAlreadyExists(string message = "Entity Already Exists.", Exception innerException = null) : base(message, innerException)
            {
            }
        }

        public class ExpiredPasswordException : Exception
        {
            public ExpiredPasswordException(string message = "Unauthorized access.", Exception innerException = null) : base(message, innerException)
            {
            }
        }
        public class UserBlockedException : Exception
        {
            public UserBlockedException(string message = "Unauthorized access.", Exception innerException = null) : base(message, innerException)
            {
            }
        }
        public class CreateUserException : Exception
        {
            public CreateUserException(string message = "Unauthorized access.", Exception innerException = null) : base(message, innerException)
            {
            }
        }
        public class ResetPasswordException : Exception
        {
            public ResetPasswordException(string message = "Bad Request.", Exception innerException = null) : base(message, innerException)
            {
            }
        }
        
    }
    
}
