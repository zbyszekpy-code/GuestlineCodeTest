using System.Collections;
using GuestlineCodeTest.Repositories.InputModel;

namespace GuestlineCodeTest.UnitTests.Repositories.Booking;

public class BookingRepositoryTestData : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        var startDate = DateTime.Today;
        var midDate = DateTime.Today.AddDays(10);
        var endDate = DateTime.Today.AddDays(100);
        // one day booked
        yield return
        [
            new[]
            {
                new BookingDto
                {
                    HotelId = HotelId,
                    RoomType = RoomType,
                    Arrival = midDate.ToString(Constants.DateFormat),
                    Departure = midDate.AddDays(1).ToString(Constants.DateFormat)
                }
            },
            HotelId,
            RoomType,
            startDate,
            endDate,
            new[]
            {
                new Application.Booking(HotelId,midDate, midDate.AddDays(1), RoomType)
            }
        ];
        // booking started before to
        yield return
        [
            new[]
            {
                new BookingDto
                {
                    HotelId = HotelId,
                    RoomType = RoomType,
                    Arrival = startDate.AddDays(-10).ToString(Constants.DateFormat),
                    Departure = midDate.ToString(Constants.DateFormat)
                }
            },
            HotelId,
            RoomType,
            startDate,
            endDate,
            new[]
            {
                new Application.Booking(HotelId,startDate.AddDays(-10), midDate, RoomType)
            }
        ];
        // booking finished after to
        yield return
        [
            new[]
            {
                new BookingDto
                {
                    HotelId = HotelId,
                    RoomType = RoomType,
                    Arrival = midDate.ToString(Constants.DateFormat),
                    Departure = endDate.AddDays(10).ToString(Constants.DateFormat)
                }
            },
            HotelId,
            RoomType,
            startDate,
            endDate,
            new[]
            {
                new Application.Booking(HotelId,midDate, endDate.AddDays(10), RoomType)
            }
        ];

        // booking started and finished after to
        yield return
        [
            new[]
            {
                new BookingDto
                {
                    HotelId = HotelId,
                    RoomType = RoomType,
                    Arrival = endDate.AddDays(10).ToString(Constants.DateFormat),
                    Departure = endDate.AddDays(11).ToString(Constants.DateFormat)
                }
            },
            HotelId,
            RoomType,
            startDate,
            endDate,
            Array.Empty<Application.Booking>()
        ];

        // booking started and finished before to
        yield return
        [
            new[]
            {
                new BookingDto
                {
                    HotelId = HotelId,
                    RoomType = RoomType,
                    Arrival = startDate.AddDays(-11).ToString(Constants.DateFormat),
                    Departure = startDate.AddDays(-10).ToString(Constants.DateFormat)
                }
            },
            HotelId,
            RoomType,
            startDate,
            endDate,
            Array.Empty<Application.Booking>()
        ];
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    private const string HotelId = "1";
    private const string RoomType = "STD";
}
