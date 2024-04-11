using GBC_Travel_Group_90.Areas.TravelManagement.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Filters;

namespace GBC_Travel_Group_90.Filters
{
    public class LoggingFilter : ActionFilterAttribute, IActionFilter
    {
        private readonly ILogger<LoggingFilter> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        public LoggingFilter(ILogger<LoggingFilter> logger, UserManager<ApplicationUser> userManager)
        {
            _logger = logger;
            _userManager = userManager;
        }


        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var requestPath = context.HttpContext.Request.Path;
            _logger.LogInformation("---------Request on path: {requestPath} at {DateTime}", requestPath, DateTime.UtcNow);
            
            var user = _userManager.GetUserAsync(context.HttpContext.User).Result;

            var routeData = context.RouteData.Values;
            var searchParams = ExtractSearchParams(context.HttpContext.Request);

            if (user != null)
            {
                // Log search info
                LogSearchInfo(user, searchParams);
            }
            else
            {
                _logger.LogInformation("---------Request Details: Anonymous User searched for: {searchParams}", string.Join(", ", searchParams.Select(kv => $"{kv.Key}={kv.Value}")));

            }
        }

        private Dictionary<string, string> ExtractSearchParams(HttpRequest request)
        {
            var searchParams = new Dictionary<string, string>();

            var searchType = request.Query["searchType"].ToString(); 

            switch (searchType)
            {
                case "Flight":
                    ExtractFlightSearchParams(request, searchParams);
                    break;
                case "Car":
                    ExtractCarSearchParams(request, searchParams);
                    break;
                case "Hotel":
                    ExtractHotelSearchParams(request, searchParams);
                    break;
                default:
                    break;
            }

            return searchParams;
        }

        private void ExtractFlightSearchParams(HttpRequest request, Dictionary<string, string> searchParams)
        {
            searchParams["origin"] = request.Query["origin"].ToString();
            searchParams["destination"] = request.Query["destination"].ToString();
            searchParams["departureDate"] = request.Query["departureDate"].ToString();
            searchParams["arrivalDate"] = request.Query["arrivalDate"].ToString();
        }

        private void ExtractCarSearchParams(HttpRequest request, Dictionary<string, string> searchParams)
        {
            searchParams["RentalCompany"] = request.Query["name"].ToString();
            searchParams["CarModel"] = request.Query["location"].ToString();
            searchParams["PickUpDate"] = request.Query["pickUpDate"].ToString();
            searchParams["DropOffDate"] = request.Query["dropOffDate"].ToString();
        }

        private void ExtractHotelSearchParams(HttpRequest request, Dictionary<string, string> searchParams)
        {
            searchParams["name"] = request.Query["name"].ToString();
            searchParams["location"] = request.Query["location"].ToString();
            searchParams["starRate"] = request.Query["starRate"].ToString();
            searchParams["maxPrice"] = request.Query["maxPrice"].ToString();
        }

        private void LogSearchInfo(ApplicationUser user, Dictionary<string, string> searchParams)
        {
            // Log the search parameters 
            // **** adapt Serilog later
            _logger.LogInformation("--------Request Details: User {userId} searched for: {searchParams}", user.Id, string.Join(", ", searchParams.Select(kv => $"{kv.Key}={kv.Value}")));


        }




        public override void OnActionExecuted(ActionExecutedContext context)
        {

            var responseStatusCode = context.HttpContext.Response.StatusCode;
            var responsePath = context.HttpContext.Request.Path;


            _logger.LogInformation("--------Response Status Code: {statuscode} on path {path} at {DateTime}",responseStatusCode, responsePath, DateTime.UtcNow);

            base.OnActionExecuted(context);
        }


        
        
    }
}
