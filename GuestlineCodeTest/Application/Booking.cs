namespace GuestlineCodeTest.Application;

public class Booking(string hotelId, DateTime from, DateTime to, string roomType)
{
    public string HotelId => hotelId;
    public DateTime From { get; } = from;
    public DateTime To { get; } = to;
    public string RoomType { get; } = roomType;

    public bool IsBooked(DateTime date)
    {
        return date >= From && date < To;
    }
}
