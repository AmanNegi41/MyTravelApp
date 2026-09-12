using Microsoft.AspNetCore.Mvc;

namespace MyTravelApp.Web.Controllers
{
    public class BookingsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
