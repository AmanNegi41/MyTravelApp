using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTravelApp.Models.Models
{
    internal class FlightSearch
    {
    }

    public class FlightSearchRequest
    {
        public string Origin { get; set; }
        public string Destination { get; set; }
        public DateOnly DepartureDate { get; set; }
        public DateOnly ReturnDate { get; set; }
        public string TripType { get; set; }
        public int Adults { get;set; }
        public int Children { get;set; }
        public int Infants { get; set; }
        public string CabinClass { get; set; }
        public string Currency { get;set; }
        public bool NonStop { get;set; }

    }
}
