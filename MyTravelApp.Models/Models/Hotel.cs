namespace MyTravelApp.Models.Models
{
    public class Hotel
    {
        public string HotelId { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public string Provider { get; set; } = string.Empty;
        public string Price { get; set; } = string.Empty;
        public decimal PriceAmount { get; set; }
        public double Rating { get; set; }
        public int ReviewCount { get; set; }
        public string? ImageUrl { get; set; }
    }
}