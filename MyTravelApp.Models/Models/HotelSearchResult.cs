using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTravelApp.Models.Models
{
        public class HotelSearchResult
        {
            public string Destination { get; set; } = string.Empty;

            public string CityId { get; set; } = string.Empty;

            public string CheckInDate { get; set; } = string.Empty;

            public string CheckOutDate { get; set; } = string.Empty;

            public int Adults { get; set; }

            public List<Hotel> Hotels { get; set; } = new();
        }
}
