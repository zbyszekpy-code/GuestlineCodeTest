using GuestlineCodeTest.Application;
using GuestlineCodeTest.Repositories.InputModel;

namespace GuestlineCodeTest.Repositories;

internal class HotelRepository : IHotelRepository
{
    private readonly IReadOnlyCollection<HotelDto> _hotels;
    public HotelRepository(IReadOnlyCollection<HotelDto> dtos)
    {
        _hotels = dtos;
    }

    public int GetRoomCount(string hotelId, string roomType)
    {
        return _hotels.Where(h => h.Id == hotelId).SelectMany(h => h.Rooms).Count(r => r.RoomType == roomType);
    }
}