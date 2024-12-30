namespace GuestlineCodeTest.Application.Availability;

public record AvailabilityQuery(string HotelId, DateTime From, DateTime To, string RoomType);
public record AvailabilityQueryResult(string HotelId, string RoomType, int AvailableRooms);

public class AvailabilityCommandHandler(IHotelsRepository hotelsRepository, IBookingRepository bookingRepository)
{
    public AvailabilityQueryResult Handle(AvailabilityQuery query)
    {
        var roomCount =  GetRoomCount(query);
        if (roomCount == 0)
        {
            return new AvailabilityQueryResult(query.HotelId, query.RoomType, 0);

        }

        var bookings = bookingRepository.GetBookings(query.HotelId, query.From, query.To, query.RoomType);
        var maxBookingCount = FindMaxBookingCount(query, bookings);

        return new AvailabilityQueryResult(query.HotelId, query.RoomType, roomCount - maxBookingCount);
    }

    private int FindMaxBookingCount(AvailabilityQuery query, IReadOnlyCollection<Booking> bookings)
    {
        var maxBookingCount = 0;
        for(var date = query.From; date <= query.To; date = date.AddDays(1))
        {
            var bookingCount = GetBookingInDay(bookings, date);
            if (maxBookingCount < bookingCount)
            {
                maxBookingCount = bookingCount;
            }
        }

        return maxBookingCount;
    }

    private int GetBookingInDay(IReadOnlyCollection<Booking> bookings, DateTime date)
    {
        return bookings.Count(b => b.IsBooked(date));
    }

    private int GetRoomCount(AvailabilityQuery query)
    {
        return hotelsRepository.GetRoomCount(query.HotelId, query.RoomType);
    }
}