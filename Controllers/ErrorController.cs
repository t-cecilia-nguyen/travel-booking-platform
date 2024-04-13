using GBC_Travel_Group_90.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace GBC_Travel_Group_90.Controllers
{
    [Route("[controller]")]
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
            //Retrieve the exeption details
            var statusCodeResult = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();
            if (statusCodeResult != null)
            {
                switch (statusCode)
                {
                    case 404:
                        ViewBag.ErrorMessage = "Sorry, the resource you requested could not be found.";
                        ViewBag.StatusCode = 404;
                        //log
                        _logger.LogWarning($"---------Eror 404 Occured On Path = {statusCodeResult.OriginalPath} +" +
                            $"and QueryString = {statusCodeResult.OriginalQueryString}");
                        return View("NotFound");
                        

                    case 500:
                        ViewBag.ErrorMessage = "Sorry, A Server Error Has Occured.";
                        ViewBag.StatusCode = 500;

                        //log
                        _logger.LogWarning($"---------Eror 500 Occured On Path = {statusCodeResult.OriginalPath} +" +
                            $"and QueryString = {statusCodeResult.OriginalQueryString}");
                        break;
                    case 401:
                        ViewBag.ErrorMessage = "Sorry, Unauthorized access.";
                        ViewBag.StatusCode = 401;

                        //log
                        _logger.LogWarning($"---------Eror 401: Unauthorized Access Error Has Occured On Path = {statusCodeResult.OriginalPath} +" +
                            $"and QueryString = {statusCodeResult.OriginalQueryString}");
                        break;
                    case 400:
                        ViewBag.ErrorMessage = "Sorry, Bad request.";
                        ViewBag.StatusCode = 400;

                        //log
                        _logger.LogWarning($"---------Eror 400: Bad Request Error Has Occured On Path = {statusCodeResult.OriginalPath} +" +
                            $"and QueryString = {statusCodeResult.OriginalQueryString}");
                        break;

                    default:
                        ViewBag.ErrorMessage = "Sorry, Unknown Error.";
                        ViewBag.StatusCode = 500;

                        //log
                        _logger.LogWarning($"---------Unknown error Has Occured On Path = {statusCodeResult.OriginalPath} +" +
                            $"and QueryString = {statusCodeResult.OriginalQueryString}");
                        break;
                }
            }
            return View("StatusCodeError");
        }
        

        [AllowAnonymous]
        [Route("Error")]
        public IActionResult Error()
        {
            //Retrieve the exeption details
            var exceptionHandlerPathFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();

            
            if (exceptionHandlerPathFeature != null)
            {
                _logger.LogError($" Request path {exceptionHandlerPathFeature.Path} threw an exception {exceptionHandlerPathFeature.Error.Message} with StackTrace:\n {exceptionHandlerPathFeature.Error.StackTrace} ");

            }
             return View("Error");
        }




    }
}
