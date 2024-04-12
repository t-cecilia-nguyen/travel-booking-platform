using GBC_Travel_Group_90.Models;
using GBC_Travel_Group_90.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace GBC_Travel_Group_90.Filters
{
    public class ValidateModelFilter :ActionFilterAttribute
    {
        private readonly ILogger<ValidateModelFilter> _logger;

        public ValidateModelFilter(ILogger<ValidateModelFilter> logger)
        {
            _logger = logger;   
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {

            if(!context.ModelState.IsValid)
            {

                _logger.LogError("-------Error 404 Occured. InValid Model State on Path {0}", context.HttpContext.Request.Path);
               // var errors = context.ModelState.AsEnumerable();


                // Get all model errors from the ModelState
                var errors = context.ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                // Create a BadRequestObjectResult with the error messages
                var result = new BadRequestObjectResult(errors);

                // Set the result in the context to return the error response
                context.Result = result;

            }

        }

        /// <inheritdoc />
        public override void OnActionExecuted(ActionExecutedContext context)
        {
        }
    }
}
