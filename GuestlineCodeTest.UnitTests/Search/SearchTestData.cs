using System.Collections;
using GuestlineCodeTest.Application.Search;

namespace GuestlineCodeTest.UnitTests.Search;

public class SearchTestData : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        DateTime startDate = DateTime.Today;
        DateTime midDate = DateTime.Today.AddDays(10);
        DateTime endDate = DateTime.Today.AddDays(100);
        // one day booked
        yield return
            [
                new[] { new Booking(HotelId, midDate, midDate.AddDays(1), RoomType), },
                1,
                new SearchQuery(HotelId, startDate, endDate, RoomType),
                new SearchQueryResult(HotelId, RoomType,
                [
                    new (startDate, midDate.AddDays(-1), 1),
                    new (midDate.AddDays(1), endDate, 1),
                ])
            ];

        // one day overbooking
        yield return
            [
                new[] { new Booking(HotelId, midDate, midDate.AddDays(1), RoomType), new Booking(HotelId, midDate, midDate.AddDays(1), RoomType), },
                1,
                new SearchQuery(HotelId, startDate, endDate, RoomType),
                new SearchQueryResult(HotelId, RoomType,
                [
                    new (startDate, midDate.AddDays(-1), 1),
                    new (midDate.AddDays(1), endDate, 1),
                ])
            ];


        // week with two short booking
        yield return
        [
            new[] { new Booking(HotelId, midDate, midDate.AddDays(1), RoomType), new Booking(HotelId, midDate.AddDays(3), midDate.AddDays(4), RoomType),},
            1,
            new SearchQuery(HotelId, startDate, endDate, RoomType),
            new SearchQueryResult(HotelId, RoomType, [
                new (startDate, midDate.AddDays(-1), 1),
                new (midDate.AddDays(1), midDate.AddDays(2), 1),
                new (midDate.AddDays(4), endDate, 1),
            ])
        ];

        // 2 rooms
        // one busy day
        yield return
        [
            new[] { new Booking(HotelId, startDate, startDate.AddDays(1), RoomType), },
            2,
            new SearchQuery(HotelId, startDate, endDate, RoomType),
            new SearchQueryResult(HotelId, RoomType, [new (startDate, endDate, 1)])
        ];

        // week with two short booking
        yield return
        [
            new[] { new Booking(HotelId, startDate, startDate.AddDays(1), RoomType), new Booking(HotelId, startDate.AddDays(3), startDate.AddDays(4), RoomType),},
            2,
            new SearchQuery(HotelId, startDate, endDate, RoomType),
            new SearchQueryResult(HotelId, RoomType, [new (startDate, endDate, 1)])
        ];

        // one busy day
        yield return
        [
            new[] { new Booking(HotelId, midDate, midDate.AddDays(1), RoomType), new Booking(HotelId, midDate, midDate.AddDays(1), RoomType), },
            2,
            new SearchQuery(HotelId, startDate, endDate, RoomType),
            new SearchQueryResult(HotelId, RoomType, [ new (startDate, midDate.AddDays(-1), 2), new (midDate.AddDays(1), endDate, 2)])
        ];

        // no rooms
        yield return
        [
            Array.Empty<Booking>(),
            0,
            new SearchQuery(HotelId, startDate, endDate, RoomType),
            new SearchQueryResult(HotelId, RoomType, [])
        ];
        yield return
        [
            Array.Empty<Booking>(),
            1,
            new SearchQuery(HotelId, startDate, endDate, RoomType),
            new SearchQueryResult(HotelId, RoomType, [new (startDate, endDate, 1)])
        ];
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    private const string HotelId = "1";
    private const string RoomType = "STD";
}