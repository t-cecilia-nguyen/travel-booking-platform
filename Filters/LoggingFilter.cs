using Azure.Core;
using GBC_Travel_Group_90.Areas.TravelManagement.Models;
using GBC_Travel_Group_90.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;

namespace GBC_Travel_Group_90.Filters
{
    public class LoggingFilter : ActionFilterAttribute, IActionFilter
    {
        private readonly ILogger<LoggingFilter> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;
        public LoggingFilter(ILogger<LoggingFilter> logger, UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _logger = logger;
            _userManager = userManager;
            _context = context;
        }


        public override async void OnActionExecuting(ActionExecutingContext context)
        {
            var requestPath = context.HttpContext.Request.Path;
            _logger.LogInformation("---------Request on path: {requestPath} at {DateTime}", requestPath, DateTime.UtcNow);

            var actionDescriptor = context.ActionDescriptor as ControllerActionDescriptor;
            if (actionDescriptor != null) {
                if(  actionDescriptor.ActionName == "Search" || actionDescriptor.ActionName == "SearchHotel")
                {
                    var routeData = context.RouteData.Values;
                    var searchParams = ExtractSearchParams(context.HttpContext.Request);


                    if (LogSearchInfo(context, searchParams))
                    {
                        // Log search activities
                        LogSearchInfo(context, searchParams);
                    }


                }

                if (actionDescriptor.ActionName == "Book" || actionDescriptor.ActionName == "Success" || actionDescriptor.ActionName == "Create" || actionDescriptor.ActionName == "CreateBooking" || actionDescriptor.ActionName == "BookFlight")
                {

                    //log booking service's name
                    var action = actionDescriptor.ActionName;
                    var user = await _userManager.FindByNameAsync(action);  
                    if (user != null)
                    {
                        _logger.LogInformation("--------- Booking Request: User {userId} is using booking {serviceType} service. ", user.Id, action);

                    }
                    else
                    {
                        _logger.LogInformation("--------- Booking Request: Anonymous User is using booking {serviceType} service", action);
                    }
                }
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

        private static void ExtractFlightSearchParams(HttpRequest request, Dictionary<string, string> searchParams)
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

        private bool LogSearchInfo (ActionExecutingContext context,Dictionary<string, string> searchParams)
        {
            // Log the search parameters 
            // 

            var user = _userManager.GetUserAsync(context.HttpContext.User).Result;

            if (!searchParams.IsNullOrEmpty())
            {
                if (user != null)
                {
                    _logger.LogInformation("-------- Searching Request: User {userId} searched for: {searchParams}", user.Id, string.Join(", ", searchParams.Select(kv => $"{kv.Key}={kv.Value}")));

                }
                else
                { 
                    _logger.LogInformation("--------- Seraching Request : Anonymous User searched for: {searchParams}", string.Join(", ", searchParams.Select(kv => $"{kv.Key}={kv.Value}")));
                }
                return true;
            }
            return false;
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
