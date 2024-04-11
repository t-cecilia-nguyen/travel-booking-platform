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


        public override void OnActionExecuting(ActionExecutingContext context)
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

                if (actionDescriptor.ActionName == "Book" || actionDescriptor.ActionName == "Success" || actionDescriptor.ActionName == "Create" || actionDescriptor.ActionName == "BookFlight")
                {
                    var bookingInfo = GetBookingDetails<Booking>(context.HttpContext);

                    // Log Booking activities
                    LogBookingInfo(context, bookingInfo);
                }
            }


        }




        private bool LogBookingInfo<T>(ActionExecutingContext context, T bookingInfo)
        {
            var user = _userManager.GetUserAsync(context.HttpContext.User).Result;

            if (bookingInfo != null)
            {
                string bookingDetails = GetBookingDetails<Booking>(context.HttpContext);

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



        /*
        private string GetBookingDetails<T>(HttpContext httpContext)
        {
            var request = httpContext.Request;
            string bookingInfo;
            T? bookingType = default;





            var routeData = httpContext.GetRouteData();
            if (routeData != null)
            {
                if (routeData.Values.TryGetValue("id", out object idValue))
                {
                    if (int.TryParse(idValue?.ToString(), out int id))
                    {
                        // Use the id variable here
                        // For example, query your database with this id to get booking details
                    }
                }
            }




            // Extract controller name from the context
            var controllerName = httpContext.GetRouteData().Values["controller"]?.ToString();

            // Check if the request contains the booking ID parameter with different names based on the controller
            if (controllerName == "Booking" && request.Query.ContainsKey("id"))
            {
                // Assign BookingType based on the controller
                bookingType = (T)(object)new Booking();

                if (int.TryParse(request.Query["id"], out int bookingId))
                {
                    bookingInfo = bookingInfo = RetriveBooking(bookingId, bookingType);

                }
                else
                {
                    bookingInfo = "Invalid booking ID format.";
                }
            }
            else if (controllerName == "HotelBooking" && request.Query.ContainsKey("hotelId"))
            {
                // Assign BookingType based on the controller
                bookingType = (T)(object)new HotelBooking();

                if (int.TryParse(request.Query["hotelId"], out int bookingId))
                {
                    bookingInfo = bookingInfo = RetriveBooking(bookingId, bookingType);

                }
                else
                {
                    bookingInfo = "Invalid booking ID format.";
                }
            }
            else if (controllerName == "CarRental" && request.Query.ContainsKey("id"))
            {
                // Assign BookingType based on the controller
                bookingType = (T)(object)new CarSuccess();

                if (int.TryParse(request.Query["id"], out int bookingId))
                {
                    bookingInfo = RetriveBooking(bookingId, bookingType);
                }
                else
                {
                    bookingInfo = "Invalid booking ID format.";
                }
            }
            else
            {
                // Handle case when the controller or booking ID parameter is not present or unrecognized
                bookingInfo = "Booking ID or Controller not found or unrecognized in the request.";
            }

            // Return the booking information as a string
            return bookingInfo;
        }
        */





        private string GetBookingDetails<T>(HttpContext httpContext)
        {
            var request = httpContext.Request;
            string bookingInfo;
            T? bookingType = default;

            // Extract controller name from the context
            var controllerName = httpContext.GetRouteData().Values["controller"]?.ToString();

            // Extract the booking ID from route data based on the controller name
            if (controllerName == "Booking" && httpContext.GetRouteData().Values.TryGetValue("id", out object bookingIdObj))
            {
                if (int.TryParse(bookingIdObj?.ToString(), out int bookingId))
                {
                    // Assign BookingType based on the controller
                    bookingType = (T)(object)new Booking();

                    bookingInfo = RetriveBooking(bookingId, bookingType);
                }
                else
                {
                    bookingInfo = "Invalid booking ID format.";
                }
            }
            //else if (controllerName == "HotelBooking" && httpContext.GetRouteData().Values.TryGetValue("hotelId", out object hotelIdObj))
            else if (controllerName == "HotelBooking" && httpContext.Request.Query.ContainsKey("hotelId"))
               
            {
                if (int.TryParse(request.Query["hotelId"], out int bookingId))
                {
                     bookingInfo = RetriveBooking(bookingId, bookingType);

                    bookingType = (T)(object)new HotelBooking();

                }
                else
                {
                    bookingInfo = "Invalid booking ID format.";
                }
            }
            else if (controllerName == "CarRental" && httpContext.GetRouteData().Values.TryGetValue("id", out object carIdObj))
             
            {
                if (int.TryParse(carIdObj?.ToString(), out int bookingId))
                {
                    // Assign BookingType based on the controller
                    bookingType = (T)(object)new CarSuccess();

                    bookingInfo = RetriveBooking(bookingId, bookingType);
                }
                else
                {
                    bookingInfo = "Invalid booking ID format.";
                }
            }
            else
            {
                // Handle case when the controller or booking ID parameter is not present or unrecognized
                bookingInfo = "Booking ID or Controller not found or unrecognized in the request.";
            }

            // Return the booking information as a string
            return bookingInfo;
        }


        private string RetriveBooking<T>(int bookingId, T bookingType)
        {

            // You can customize this method based on the structure of your booking models
            if (bookingType is HotelBooking)
            {
                var booking = bookingType as HotelBooking;
                var hotel = booking?.Hotel;
                return $"Hotel Name: {hotel?.Name}, ID: {booking?.HotelBookingId}, Number of Rooms: {booking?.NumOfRoomsToBook}, Booking Date: {booking?.BookingDate}";
            }
            else if (bookingType is Booking)
            {
                var booking = bookingType as Booking;
                var flight = booking?.Flight;

                return $"Flight Id: {booking?.FlightId}, Booking ID: {booking?.BookingId}, Ariline: {flight?.Airline}, Origin: {flight?.Origin}, Destination: {flight?.Destination}, Departure: {flight?.DepartureTime}, Arrival: {flight?.ArrivalTime}";

            }
            else if (bookingType is CarSuccess)
            {
                var booking = bookingType as CarSuccess;
                var car = booking?.CarRental;

                return $"Model: {car?.CarModel}, Booking ID: {booking?.Id}, Pick-up Date: {booking?.CarRental.PickUpDate}, Pick-up Loaction: {booking?.CarRental.PickUpLocation}";
            }
            else
            {
                return "Unknown Booking Details";
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



        public override void OnActionExecuted(ActionExecutedContext context)
        {

            var responseStatusCode = context.HttpContext.Response.StatusCode;
            var responsePath = context.HttpContext.Request.Path;


            _logger.LogInformation("--------Response Status Code: {statuscode} on path {path} at {DateTime}",responseStatusCode, responsePath, DateTime.UtcNow);

            base.OnActionExecuted(context);
        }


        
        
    }
}
