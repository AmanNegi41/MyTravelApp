using Microsoft.AspNetCore.Mvc;
using MyTravelApp.Web.Models;

namespace MyTravelApp.Web.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            var model = DashboardViewModel.SampleData("aman");
            return View(model);
        }
    }
}
