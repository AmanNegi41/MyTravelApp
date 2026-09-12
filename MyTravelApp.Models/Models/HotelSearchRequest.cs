namespace MyTravelApp.Models.Models
{
    public class HotelSearchRequest
    {
        public string CityCode { get; set; } = string.Empty;
        public int Adults { get; set; } = 2;
        public int Rooms { get; set; } = 1;
        public string CheckInDate { get; set; } = string.Empty;
        public string CheckOutDate { get; set; } = string.Empty;
    }
}