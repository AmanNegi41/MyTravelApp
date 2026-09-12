using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Runtime.InteropServices;
using MyTravelApp.Services;
using System.Text.Json;

namespace MyTravelApp.Web.Controllers
{
    public class WeatherController : Controller
    {

        private readonly WeatherSearchService _weatherSearchService;

        public WeatherController(WeatherSearchService weatherSearchService)
        {
            _weatherSearchService = weatherSearchService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> WeatherSearch(string location)
        {
         var data = await _weatherSearchService.WeatherSearch(location);
         var dt = JsonSerializer.Deserialize<object>(data);
         return View(dt);
        }
    }
}
