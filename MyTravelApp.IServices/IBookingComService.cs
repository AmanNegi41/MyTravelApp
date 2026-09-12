using System.Threading.Tasks;
using MyTravelApp.Models.Models;

namespace MyTravelApp.IServices
{
    public interface IBookingComService
    {
        Task<HotelSearchResult> SearchHotelsAsync(string destination, string checkIn, string checkOut, int adults);
    }
}