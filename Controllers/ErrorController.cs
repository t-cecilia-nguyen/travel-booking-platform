using GBC_Travel_Group_90.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace GBC_Travel_Group_90.Controllers
{
    public class ErrorController : Controller
    {
        private readonly ILogger<ErrorController> _logger;
        public ErrorController(ILogger<ErrorController> logger)
        {
            _logger = logger;
        }

        [AllowAnonymous]
        [Route("Error/{statusCode}")]
        public IActionResult HttpStatusCodeHandler(int statusCode)
        {

            //Hanlde unsuccesful status code

            //Retrieve the exeption details
            var statusCodeResult = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();

            var errorPath = statusCodeResult?.OriginalPath;
            var errorQS = statusCodeResult?.OriginalQueryString;

            switch (statusCode)
            {
                case 404:
                    ViewBag.ErrorMessage = "Sorry, the resource you requested could not be found.";
                    ViewBag.StatusCode = 404;
                    
                    //log
                    _logger.LogWarning("---------Eror 404; Path {path}; Message: the resource you requested could not be found; QS: {query}", errorPath, errorQS);
                    break;

                case 500:
                    ViewBag.ErrorMessage = "Sorry, A Server Error Has Occured.";
                    ViewBag.StatusCode = 500;

                    //log
                    _logger.LogWarning($"---------Eror 500; Path: {errorPath}; Message: A Server Error Has Occured; QS: {errorQS}");
                    break;
                case 401:
                    ViewBag.ErrorMessage = "Sorry, Unauthorized access.";
                    ViewBag.StatusCode = 401;

                    //log
                    _logger.LogWarning($"---------Eror 401; Path:{errorPath}; Message: Unauthorized Access Error ; QS: {errorQS}");
                    break;
                case 400:
                    ViewBag.ErrorMessage = "Sorry, Bad request.";
                    ViewBag.StatusCode = 400;

                    //log
                    _logger.LogWarning($"---------Eror 400; Path {errorPath} ;Message: Bad Request Error Has Occured ; QS: {errorQS}");
                    break;

                default:
                    ViewBag.ErrorMessage = "Sorry, Unknown Error.";
                    ViewBag.StatusCode = 500;

                    //log
                    _logger.LogWarning($"---------Error 500; Path{errorPath}; Message: Unknown error Has Occured; QS: {errorQS}");
                    break;
            }
            
            return View("StatusCodeError");
        }
        

        [AllowAnonymous]
        [Route("Error")]
        public IActionResult Error()
        {
            //Handle Any unhandled exceptions

            //Retrieve the exeption details
            var exceptionHandlerPathFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            if (exceptionHandlerPathFeature != null)
            {
                ViewBag.ExceptionPath = exceptionHandlerPathFeature.Path;
                ViewBag.ExceptionMessage = exceptionHandlerPathFeature.Error.Message;

                _logger.LogError($"Exception path: {exceptionHandlerPathFeature.Path}; Exception Message: {exceptionHandlerPathFeature.Error.Message} StackTrace: {exceptionHandlerPathFeature.Error.StackTrace}");

            }

            // Retrieve the exception details from TempData from middleware 
            string exceptionSource = HttpContext.Session.GetString("ExceptionSource");
            string exceptionMessage = HttpContext.Session.GetString("ExceptionMessage");
            

            // Clear the TempData after retrieving the values
            HttpContext.Session.Remove("ExceptionSource");
            HttpContext.Session.Remove("ExceptionMessage");
            

            // Pass the exception details to the view
            ViewBag.ExceptionSoure = exceptionSource;
            ViewBag.ExceptionMessage = exceptionMessage;
            
           

            return View("Error");
        }




    }
}
