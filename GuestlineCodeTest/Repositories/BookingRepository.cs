using GuestlineCodeTest.Application;
using GuestlineCodeTest.Repositories.InputModel;

namespace GuestlineCodeTest.Repositories;

internal class BookingRepository : IBookingRepository
{
    private readonly IReadOnlyCollection<Booking> _bookings;

    public BookingRepository(IReadOnlyCollection<BookingDto> dtos)
    {
        _bookings = dtos.Select(dto => new Booking(dto.HotelId,
                DateTime.ParseExact(dto.Arrival, Constants.DateFormat, CultureInfo.InvariantCulture),
                DateTime.ParseExact(dto.Departure, Constants.DateFormat, CultureInfo.InvariantCulture),
                dto.RoomType))
            .ToArray();
    }

    public IReadOnlyCollection<Booking> GetBookings(string hotelId, DateTime from, DateTime to, string roomType)
    {
        return _bookings.Where(b => b.HotelId == hotelId && b.To >= from && b.From < to && b.RoomType == roomType).ToArray();
    }
}