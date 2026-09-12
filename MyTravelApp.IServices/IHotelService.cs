using MyTravelApp.Models.Models;

namespace MyTravelApp.IServices
{
    public interface IHotelService
    {
        Task<HotelSearchResult> SearchHotelsAsync(HotelSearchRequest request, CancellationToken cancellationToken = default);
    }
}
