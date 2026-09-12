using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Runtime.InteropServices;
using MyTravelApp.Services;
using System.Text.Json;
using MyTravelApp.Web.Models;

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
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            WeatherView dtList = JsonSerializer.Deserialize<WeatherView>(data, options);
            return PartialView("_WeatherIndex", dtList);
        }
    }
}
