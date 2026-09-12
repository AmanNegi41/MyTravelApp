using Microsoft.AspNetCore.Mvc;
using MyTravelApp.Models.Models;
using MyTravelApp.Services;
using MyTravelApp.IServices;

namespace MyTravelApp.Web.Controllers
{
    public class HotelController : Controller
    {
        private readonly IBookingComService _bookingService; // Use interface if registered
        private readonly ILogger<HotelController> _logger;

        public HotelController(IBookingComService bookingService, ILogger<HotelController> logger)
        {
            _bookingService = bookingService;
            _logger = logger;
        }

        // 1. Handles the form submission when user clicks "Search"
        [HttpPost]
        public async Task<IActionResult> HotelSearch(HotelSearchRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.CityCode))
            {
                TempData["HotelSearchError"] = "Please enter a destination city.";
                return RedirectToAction("Index", "Dashboard");
            }

            try
            {
                var result = await _bookingService.SearchHotelsAsync(
                    request.CityCode,
                    request.CheckInDate,
                    request.CheckOutDate,
                    request.Adults > 0 ? request.Adults : 2
                );

                // Render the HotelResults.cshtml view directly with data
                return View("HotelResults", result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Booking.com search failed for {City}", request.CityCode);
                TempData["HotelSearchError"] = ex.Message;
                return RedirectToAction("Index", "Dashboard");
            }
        }

        // 2. Handles direct URL navigation / page refresh
        [HttpGet]
        public IActionResult HotelSearch()
        {
            return RedirectToAction("Index", "Dashboard");
        }

    }
}