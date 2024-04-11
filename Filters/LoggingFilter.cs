using GBC_Travel_Group_90.Areas.TravelManagement.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;
using System;

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

           
            var routeData = context.RouteData.Values;
            var searchParams = ExtractSearchParams(context.HttpContext.Request);
            var bookingInfo = GetBookingDetails(context.HttpContext.Request);

            if (LogSearchInfo( context, searchParams))
            {
                // Log search activities
                LogSearchInfo(context,  searchParams);
            }
            if(LogBookingInfo(context, bookingInfo))
            {
                // Log Booking activities
                LogBookingInfo(context, bookingInfo);
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

        private bool LogSearchInfo (ActionExecutingContext context,Dictionary<string, string> searchParams)
        {
            // Log the search parameters 
            // 

            var user = _userManager.GetUserAsync(context.HttpContext.User).Result;

            if (!searchParams.IsNullOrEmpty())
            {
                if (user != null)
                {
                    _logger.LogInformation("--------Request Searching: User {userId} searched for: {searchParams}", user.Id, string.Join(", ", searchParams.Select(kv => $"{kv.Key}={kv.Value}")));

                }
                else
                { 
                    _logger.LogInformation("---------Request Details: Anonymous User searched for: {searchParams}", string.Join(", ", searchParams.Select(kv => $"{kv.Key}={kv.Value}")));
                }
                return true;
            }
            return false;
        }



        private bool LogBookingInfo<T>(ActionExecutingContext context, T bookingInfo)
        {
            var user = _userManager.GetUserAsync(context.HttpContext.User).Result;

            if (bookingInfo != null)
            {
                string bookingDetails = GetBookingDetails(bookingInfo);

                if (user != null)
                {
                    _logger.LogInformation("--------Request Booking: User {userId} Attempting To Book for: {bookingDetails}", user.Id, bookingDetails);
                }
                else
                {
                    _logger.LogInformation("--------Request Booking: Anonymous User  for: {bookingDetails}", bookingDetails);
                }

                return true;
            }

            return false;
        }

        private string GetBookingDetails<T>(T bookingInfo)
        {
            // You can customize this method based on the structure of your booking models
            if (bookingInfo is HotelBooking)
            {
                var booking = bookingInfo as HotelBooking;
                var hotel = booking?.Hotel;
                return $"Hotel Name: {hotel?.Name}, ID: {booking?.HotelBookingId}, Number of Rooms: {booking?.NumOfRoomsToBook}, Booking Date: {booking?.BookingDate}";
            }
            else if (bookingInfo is Booking)
            {
                var booking = bookingInfo as Booking;
                var flight = booking?.Flight;

                return $"Flight Id: {booking?.FlightId}, Booking ID: {booking?.BookingId}, Ariline: {flight?.Airline}, Origin: {flight?.Origin}, Destination: {flight?.Destination}, Departure: {flight?.DepartureTime}, Arrival: {flight?.ArrivalTime}";
               
            }
            else if (bookingInfo is CarSuccess)
            {
                var booking = bookingInfo as CarSuccess;
                var car = booking?.CarRental;

                return $"Model: {car?.CarModel}, Booking ID: {booking?.Id}, Pick-up Date: {booking?.CarRental.PickUpDate}, Pick-up Loaction: {booking?.CarRental.PickUpLocation}";
            }
            else
            {
                return "Unknown Booking Details";
            }
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
