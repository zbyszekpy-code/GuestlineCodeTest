using GuestlineCodeTest.Application.Availability;

namespace GuestlineCodeTest.Command;

public record Availability(string HotelId, string DateRange, string RoomType)
{
    public AvailabilityQuery GetCommand()
    {
        var dates = DateRange.Split('-');
        var from = DateTime.ParseExact(dates[0], Constants.DateFormat, CultureInfo.InvariantCulture);
        if (dates.Length == 1)
        {
            return new AvailabilityQuery(HotelId, from, from.AddDays(1), RoomType);
        }

        var to = DateTime.ParseExact(dates[1], Constants.DateFormat, CultureInfo.InvariantCulture);
        return new AvailabilityQuery(HotelId, from, to, RoomType);
    }
}
