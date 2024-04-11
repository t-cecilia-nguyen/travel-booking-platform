using GBC_Travel_Group_90.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

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
            //Retrieve the exeption details
            var statusCodeResult = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();
            if (statusCodeResult != null)
            {
                switch (statusCode)
                {
                    case 404:
                        ViewBag.ErrorMessage = "Sorry, the resource you requested could not be found.";
                        
                        //log
                        _logger.LogWarning($"---------Eror 404 Occured On Path = {statusCodeResult.OriginalPath} +" +
                            $"and QueryString = {statusCodeResult.OriginalQueryString}");
                        break;

                    case 500:
                        ViewBag.ErrorMessage = "Sorry, A Server Error Has Occured.";

                        //log
                        _logger.LogWarning($"---------Eror 500 Occured On Path = {statusCodeResult.OriginalPath} +" +
                            $"and QueryString = {statusCodeResult.OriginalQueryString}");
                        break;

                    default:
                        break;
                }
            }
            return View("NotFound");
        }
        

        [AllowAnonymous]
        [Route("Error")]
        public IActionResult Error()
        {
            //Retrieve the exeption details
            var exceptionHandlerPathFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();


            if (exceptionHandlerPathFeature != null)
            {
                _logger.LogError($"The Path {exceptionHandlerPathFeature.Path} threw an exception {exceptionHandlerPathFeature.Error.Message} ");

               
            }
             return View("Error");
        }




    }
}
