using System.Collections;

namespace GuestlineCodeTest.UnitTests.Availability;

public class AvailabilityTestData : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        DateTime startDate = DateTime.Today;
        // one day
        yield return
            [
                new[] { new Booking(HotelId, startDate, startDate, RoomType), },
                1,
                new AvailabilityQuery(HotelId, startDate, startDate, RoomType),
                new AvailabilityQueryResult(HotelId, RoomType, 0)
            ];

        // one day overbooking
        yield return
            [
                new[] { new Booking(HotelId, startDate, startDate, RoomType), new Booking(HotelId, startDate, startDate, RoomType), },
                1,
                new AvailabilityQuery(HotelId, startDate, startDate, RoomType),
                new AvailabilityQueryResult(HotelId, RoomType, -1)
            ];

        // one busy day
        yield return
        [
            new[] { new Booking(HotelId, startDate, startDate, RoomType), },
            1,
            new AvailabilityQuery(HotelId, startDate, startDate.AddDays(1), RoomType),
            new AvailabilityQueryResult(HotelId, RoomType, 0)
        ];
        // week with two short booking
        yield return
        [
            new[] { new Booking(HotelId, startDate, startDate, RoomType), new Booking(HotelId, startDate.AddDays(3), startDate.AddDays(3), RoomType),},
            1,
            new AvailabilityQuery(HotelId, startDate, startDate.AddDays(7), RoomType),
            new AvailabilityQueryResult(HotelId, RoomType, 0)
        ];

        // 2 rooms
        // one busy day
        yield return
        [
            new[] { new Booking(HotelId, startDate, startDate, RoomType), },
            2,
            new AvailabilityQuery(HotelId, startDate.AddDays(-1), startDate.AddDays(1), RoomType),
            new AvailabilityQueryResult(HotelId, RoomType, 1)
        ];
        // week with two short booking
        yield return
        [
            new[] { new Booking(HotelId, startDate, startDate, RoomType), new Booking(HotelId, startDate.AddDays(3), startDate.AddDays(4), RoomType),},
            2,
            new AvailabilityQuery(HotelId, startDate, startDate.AddDays(7), RoomType),
            new AvailabilityQueryResult(HotelId, RoomType, 1)
        ];

        // no rooms
        yield return
        [
            Array.Empty<Booking>(),
            0,
            new AvailabilityQuery(HotelId, startDate, startDate.AddDays(1), RoomType),
            new AvailabilityQueryResult(HotelId, RoomType, 0)
        ];
        // no booking
        yield return
        [
            Array.Empty<Booking>(),
            1,
            new AvailabilityQuery(HotelId, startDate, startDate.AddDays(1), RoomType),
            new AvailabilityQueryResult(HotelId, RoomType, 1)
        ];
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    private const string HotelId = "1";
    private const string RoomType = "STD";
}