namespace GuestlineCodeTest.Application.Search;

public record SearchQuery(string HotelId, DateTime From, DateTime To, string RoomType);
public record SearchQueryResult(string HotelId, string RoomType, IReadOnlyCollection<(DateTime From, DateTime To)> Availability);

public class SearchCommandHandler(IHotelsRepository hotelsRepository, IBookingRepository bookingRepository)
{
    public SearchQueryResult Handle(SearchQuery query)
    {
        var roomCount =  GetRoomCount(query);
        if (roomCount == 0)
        {
            return new SearchQueryResult(query.HotelId, query.RoomType, new List<(DateTime From, DateTime To)>());

        }
        var bookings = bookingRepository.GetBookings(query.HotelId, query.From, query.To, query.RoomType);


        return new SearchQueryResult(query.HotelId, query.RoomType, GetAvailability(query, roomCount, bookings).ToList());

   }

    private IEnumerable<(DateTime From, DateTime To)> GetAvailability(SearchQuery query, int roomCount, IReadOnlyCollection<Booking> bookings)
    {
        bool isAvailable = false;
        DateTime from = query.From;
        for(var date = query.From; date <= query.To; date = date.AddDays(1))
        {
            var roomAvailable = IsRoomAvailable(bookings, date, roomCount);

            if (roomAvailable && !isAvailable)
            {
                    from = date;
                    isAvailable = true;
                    continue;
            }

            if (!roomAvailable && isAvailable)
            {
                yield return (from, date.AddDays(-1));
                isAvailable = false;
                continue;
            }
        }

        if (isAvailable)
        {
            yield return (from, query.To);
        }
    }

    private bool IsRoomAvailable(IReadOnlyCollection<Booking> bookings, DateTime date, int roomCount)
    {
        return bookings.Count(b => b.IsBooked(date)) < roomCount;
    }

    private int GetRoomCount(SearchQuery query)
    {
        return hotelsRepository.GetRoomCount(query.HotelId, query.RoomType);
    }
}