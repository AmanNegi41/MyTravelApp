using Microsoft.AspNetCore.Mvc;

namespace MyTravelApp.Web.Controllers
{
    public class FlightController : Controller
    {

        //string apiKey = "55df6ba4afae988f5a70c7a3752fbec3";
        //string url = $"https://api.aviationstack.com/v1/flights?access_key={apiKey}&limit=5";



        public IActionResult Index()
        {
            return View();
        }

    }
}
