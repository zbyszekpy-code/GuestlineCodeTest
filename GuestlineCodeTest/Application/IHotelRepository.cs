namespace GuestlineCodeTest.Application;

public interface IHotelRepository
{
    int GetRoomCount(string hotelId, string roomType);
}