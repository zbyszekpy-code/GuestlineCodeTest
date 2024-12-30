namespace GuestlineCodeTest.Application;

public class Booking(string hotelId, DateTime from, DateTime to, string roomType)
{
    public bool IsBooked(DateTime date)
    {
        return date >= from && date <= to;
    }
}