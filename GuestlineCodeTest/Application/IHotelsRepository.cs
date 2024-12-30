namespace GuestlineCodeTest.Application;

public interface IHotelsRepository
{
    int GetRoomCount(string hotelId, string roomType);
}