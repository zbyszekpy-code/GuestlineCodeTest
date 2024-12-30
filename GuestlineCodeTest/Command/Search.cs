using GuestlineCodeTest.Application.Search;

namespace GuestlineCodeTest.Command;

public record Search(string HotelId, long DaysAhead, string RoomType)
{
    public SearchQuery GetCommand()
    {
        var from = DateTime.Today;
        var to = from.AddDays(DaysAhead);
        return new SearchQuery(HotelId, from, to, RoomType);
    }
}
