using System.Text.Json.Serialization;

namespace MyTravelApp.Web.Models
{
    public class BookingApiResponse
    {

        public class BookingSearchResponse
        {
            [JsonPropertyName("result")]
            public List<BookingHotelDto>? Result { get; set; }
        }

        public class BookingHotelDto
        {
            [JsonPropertyName("hotel_id")]
            public long HotelId { get; set; }

            [JsonPropertyName("hotel_name")]
            public string? HotelName { get; set; }

            [JsonPropertyName("city_trans")]
            public string? City { get; set; }

            [JsonPropertyName("review_score")]
            public double ReviewScore { get; set; }

            [JsonPropertyName("review_nr")]
            public int ReviewCount { get; set; }

            [JsonPropertyName("min_total_price")]
            public double MinTotalPrice { get; set; }

            [JsonPropertyName("currency_code")]
            public string? CurrencyCode { get; set; }

            [JsonPropertyName("main_photo_url")]
            public string? MainPhotoUrl { get; set; }
        }


    }
}
