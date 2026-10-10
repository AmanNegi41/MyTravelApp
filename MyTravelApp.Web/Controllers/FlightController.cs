using Microsoft.AspNetCore.Mvc;
using MyTravelApp.Models.Models;

namespace MyTravelApp.Web.Controllers
{
    public class FlightController : Controller
    {

        private readonly IConfiguration _configuration;
        public FlightController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        //private readonly string apiKey = "55df6ba4afae988f5a70c7a3752fbec3";
        //string url = $"https://api.aviationstack.com/v1/flights?access_key={apiKey}&limit=5";


        public IActionResult Index()
        {
            return View();
        }



        //[HttpGet]
        //public IActionResult FlightSearch([FromBody] FlightSearchRequest request)
        //{
        //}

    }
}
