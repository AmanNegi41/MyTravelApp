using Microsoft.AspNetCore.Mvc;
using MyTravelApp.Web.Models;

namespace MyTravelApp.Web.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            var bookings = new List<BookingViewModel>
        {
            new BookingViewModel { BookingId = "BK-2026-01", PassengerName = "Rajesh Kumar", ServiceType = "✈️ Flight (DEL to BOM)", Amount = 6500.00m, Status = "Success" },
            new BookingViewModel { BookingId = "BK-2026-02", PassengerName = "Priya Sharma", ServiceType = "🏨 Hotel (Taj Mumbai)", Amount = 14500.00m, Status = "Success" },
            new BookingViewModel { BookingId = "BK-2026-03", PassengerName = "Amit Verma", ServiceType = "✈️ Flight (BLR to DEL)", Amount = 5200.00m, Status = "Failed" }
        };
            return View(bookings);
        }
    }
}
