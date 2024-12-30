namespace GuestlineCodeTest.Command;

public record Availability(string HotelId, string DateRange, string RoomType);
public record Search(string HotelId, long DaysAhead, string RoomType);
