using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTravelApp.Services
{
    public class WeatherSearchService
    {
        private readonly HttpClient _httpClient;
        private const string _apiKey = "ade4b62d675e7af939734ed2b56c0e39";
        public WeatherSearchService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> WeatherSearch(string location)
        {
            string url = $"https://api.openweathermap.org/data/2.5/weather?q={location}&appid={_apiKey}";
            var result = await _httpClient.GetStringAsync(url);
            return result;
        }
    }
}

