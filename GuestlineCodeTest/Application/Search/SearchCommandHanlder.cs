namespace GuestlineCodeTest.Application.Search;

public record SearchQuery(string HotelId, DateTime From, DateTime To, string RoomType);
public record AvailabilityRange(DateTime From, DateTime To, int Count);
public record SearchQueryResult(string HotelId, string RoomType, IReadOnlyCollection<AvailabilityRange> Availability);

public class SearchCommandHandler(IHotelRepository hotelRepository, IBookingRepository bookingRepository)
{
    public SearchQueryResult Handle(SearchQuery query)
    {
        var roomCount =  GetRoomCount(query);
        if (roomCount == 0)
        {
            return new SearchQueryResult(query.HotelId, query.RoomType, new List<AvailabilityRange>());

        }
        var bookings = bookingRepository.GetBookings(query.HotelId, query.From, query.To, query.RoomType);


        return new SearchQueryResult(query.HotelId, query.RoomType, GetAvailability(query, roomCount, bookings).ToList());

   }

    private IEnumerable<AvailabilityRange> GetAvailability(SearchQuery query, int roomCount, IReadOnlyCollection<Booking> bookings)
    {
        int availableRooms = 0;
        DateTime from = query.From;
        for(var date = query.From; date <= query.To; date = date.AddDays(1))
        {
            var availableRoomsInDay = roomCount - GetBookingInDay(bookings, date);

            if (availableRoomsInDay > 0 && availableRooms == 0)
            {
                    from = date;
                    availableRooms = availableRoomsInDay;
                    continue;
            }

            if (availableRoomsInDay <= 0 && availableRooms > 0)
            {
                yield return new (from, date.AddDays(-1), availableRooms);
                availableRooms = 0;
                continue;
            }

            if (availableRoomsInDay > 0 && availableRooms > 0 && availableRooms > availableRoomsInDay)
            {
                availableRooms = availableRoomsInDay;
            }
        }

        if (availableRooms > 0)
        {
            yield return new (from, query.To, availableRooms);
        }
    }

    private int GetBookingInDay(IReadOnlyCollection<Booking> bookings, DateTime date)
    {
        return bookings.Count(b => b.IsBooked(date));
    }

    private int GetRoomCount(SearchQuery query)
    {
        return hotelRepository.GetRoomCount(query.HotelId, query.RoomType);
    }
}