using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MyTravelApp.IServices;
using MyTravelApp.Models.Models;
using Microsoft.Extensions.Caching.Memory;

namespace MyTravelApp.Services
{
    public class BookingComService : IBookingComService
    {
        private readonly HttpClient _httpClient;
        private readonly RapidApiOptions _options;
        private readonly ILogger<BookingComService> _logger;
        private readonly IMemoryCache _cache;

        public BookingComService(
            HttpClient httpClient,
            IOptions<RapidApiOptions> options,
            ILogger<BookingComService> logger,
            IMemoryCache cache)
        {
            _httpClient = httpClient;
            _options = options.Value;
            _logger = logger;
            _cache = cache;

            _httpClient.BaseAddress = new Uri("https://apidojo-booking-v1.p.rapidapi.com/");
            _httpClient.DefaultRequestHeaders.Add("X-RapidAPI-Key", _options.ApiKey ?? string.Empty);
            _httpClient.DefaultRequestHeaders.Add("X-RapidAPI-Host", "apidojo-booking-v1.p.rapidapi.com");
        }

        public async Task<HotelSearchResult> SearchHotelsAsync(string cityName, string checkInDate, string checkOutDate, int adults)
        {
            var checkIn = FormatDate(checkInDate, DateTime.Today.AddDays(7));
            var checkOut = FormatDate(checkOutDate, DateTime.Today.AddDays(8));
            var cacheKey = $"hotels_{cityName?.ToLower()}_{checkIn}_{checkOut}_{adults}";

            if (_cache.TryGetValue(cacheKey, out HotelSearchResult cachedResult))
            {
                return cachedResult;
            }

            var hotels = new List<Hotel>();

            try
            {
                var destId = await GetDestinationIdAsync(cityName);
                if (string.IsNullOrEmpty(destId))
                {
                    _logger.LogWarning("Destination ID not found for city: {CityName}", cityName);
                    return BuildResult(cityName, checkIn, checkOut, adults, hotels);
                }

                var requestUrl = $"properties/list?dest_ids={destId}" +
                                 $"&arrival_date={checkIn}&departure_date={checkOut}" +
                                 $"&guest_qty={adults}&room_qty=1&offset=0&rows=50" +
                                 $"&units=metric&locale=en-gb&currency=INR";

                using var response = await _httpClient.GetAsync(requestUrl);
                var json = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogError("APIDojo Search Error: {StatusCode} - {Body}", response.StatusCode, json);
                    return BuildResult(cityName, checkIn, checkOut, adults, hotels);
                }

                using var doc = JsonDocument.Parse(json);
                JsonElement resultsArray = doc.RootElement;

                if (doc.RootElement.ValueKind == JsonValueKind.Object && doc.RootElement.TryGetProperty("result", out var resProp))
                {
                    resultsArray = resProp;
                }

                if (resultsArray.ValueKind == JsonValueKind.Array)
                {
                    foreach (var item in resultsArray.EnumerateArray())
                    {
                        var priceStr = "N/A";
                        decimal priceAmount = 0;

                        if (item.TryGetProperty("min_total_price", out var minPriceProp) && minPriceProp.ValueKind == JsonValueKind.Number)
                        {
                            priceAmount = minPriceProp.GetDecimal();
                            priceStr = $"₹{priceAmount:N0}";
                        }
                        else if (item.TryGetProperty("price_breakdown", out var priceBreakdown) &&
                                 priceBreakdown.TryGetProperty("gross_price", out var grossPrice) &&
                                 grossPrice.ValueKind == JsonValueKind.Object)
                        {
                            priceAmount = (decimal)ReadDouble(grossPrice, "value");
                            if (priceAmount > 0)
                            {
                                priceStr = $"₹{priceAmount:N0}";
                            }
                        }

                        hotels.Add(new Hotel
                        {
                            HotelId = ReadString(item, "hotel_id"),
                            Name = ReadString(item, "hotel_name"),
                            City = cityName,
                            Location = ReadString(item, "address"),
                            Price = priceStr,
                            PriceAmount = priceAmount,
                            Rating = ReadDouble(item, "review_score"),
                            ReviewCount = (int)ReadDouble(item, "review_nr"),
                            ImageUrl = GetImageUrl(item),
                            Provider = "Booking.com"
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error in SearchHotelsAsync for {City}", cityName);
            }

            var result = BuildResult(cityName, checkIn, checkOut, adults, hotels);

            if (hotels.Any())
            {
                _cache.Set(cacheKey, result, TimeSpan.FromHours(24));
            }

            return result;
        }

        private static string GetImageUrl(JsonElement item)
        {
            string url = ReadString(item, "main_photo_url");
            if (string.IsNullOrEmpty(url)) url = ReadString(item, "photo_url");
            if (string.IsNullOrEmpty(url)) url = ReadString(item, "max_photo_url");

            if (string.IsNullOrEmpty(url)) return string.Empty;

            if (url.StartsWith("//"))
            {
                url = "https:" + url;
            }

            url = url.Replace("/square60/", "/max500/")
                     .Replace("/square180/", "/max500/")
                     .Replace("/max300/", "/max500/")
                     .Replace("/max100x100/", "/max500x500/");

            return url;
        }

        private async Task<string> GetDestinationIdAsync(string cityName)
        {
            if (string.IsNullOrWhiteSpace(cityName)) return string.Empty;

            var cacheKey = $"dest_id_{cityName.Trim().ToLower()}";
            if (_cache.TryGetValue(cacheKey, out string cachedDestId))
            {
                return cachedDestId;
            }

            try
            {
                var requestUrl = $"locations/auto-complete?text={Uri.EscapeDataString(cityName)}&languagecode=en-us";
                using var response = await _httpClient.GetAsync(requestUrl);
                var json = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogError("APIDojo Locations Error: {StatusCode} - {Body}", response.StatusCode, json);
                    return string.Empty;
                }

                using var doc = JsonDocument.Parse(json);

                JsonElement locationArray = doc.RootElement;
                if (doc.RootElement.ValueKind == JsonValueKind.Object && doc.RootElement.TryGetProperty("result", out var resProp))
                {
                    locationArray = resProp;
                }

                if (locationArray.ValueKind == JsonValueKind.Array)
                {
                    foreach (var item in locationArray.EnumerateArray())
                    {
                        var destType = ReadString(item, "dest_type");
                        if (destType == "city" || destType == "region")
                        {
                            var destId = ReadString(item, "dest_id");
                            if (!string.IsNullOrEmpty(destId))
                            {
                                _cache.Set(cacheKey, destId, TimeSpan.FromDays(30));
                            }
                            return destId;
                        }
                    }

                    var first = locationArray.EnumerateArray().FirstOrDefault();
                    if (first.ValueKind == JsonValueKind.Object)
                    {
                        var destId = ReadString(first, "dest_id");
                        if (!string.IsNullOrEmpty(destId))
                        {
                            _cache.Set(cacheKey, destId, TimeSpan.FromDays(30));
                        }
                        return destId;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching destination ID for {City}", cityName);
            }

            return string.Empty;
        }

        private static HotelSearchResult BuildResult(string city, string checkIn, string checkOut, int adults, List<Hotel> hotels) =>
            new HotelSearchResult
            {
                Destination = city,
                CheckInDate = checkIn,
                CheckOutDate = checkOut,
                Adults = adults,
                Hotels = hotels ?? new List<Hotel>()
            };

        private static string FormatDate(string dateStr, DateTime fallback)
        {
            if (!string.IsNullOrWhiteSpace(dateStr) &&
               (DateTime.TryParse(dateStr, CultureInfo.InvariantCulture, DateTimeStyles.None, out var parsed) ||
                DateTime.TryParse(dateStr, out parsed)))
            {
                return parsed.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
            }
            return fallback.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
        }

        private static string ReadString(JsonElement element, string prop) =>
            element.TryGetProperty(prop, out var val) ? val.ToString() ?? "" : "";

        private static double ReadDouble(JsonElement element, string prop)
        {
            if (element.TryGetProperty(prop, out var val) && val.ValueKind == JsonValueKind.Number)
            {
                return val.TryGetDouble(out var d) ? d : 0;
            }
            return 0;
        }
    }
}