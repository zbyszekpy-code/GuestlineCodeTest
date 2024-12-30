namespace GuestlineCodeTest.Application;

public interface IBookingRepository
{
    IReadOnlyCollection<Booking> GetBookings(string hotelId, DateTime from, DateTime to, string roomType);
}