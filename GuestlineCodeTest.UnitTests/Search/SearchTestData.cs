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
                new[] { new Booking(HotelId, midDate, midDate, RoomType), },
                1,
                new SearchQuery(HotelId, startDate, endDate, RoomType),
                new SearchQueryResult(HotelId, RoomType,
                [
                    (startDate, midDate.AddDays(-1)),
                    (midDate.AddDays(1), endDate),
                ])
            ];

        // one day overbooking
        yield return
            [
                new[] { new Booking(HotelId, midDate, midDate, RoomType), new Booking(HotelId, midDate, midDate, RoomType), },
                1,
                new SearchQuery(HotelId, startDate, endDate, RoomType),
                new SearchQueryResult(HotelId, RoomType,
                [
                    (startDate, midDate.AddDays(-1)),
                    (midDate.AddDays(1), endDate),
                ])
            ];


        // week with two short booking
        yield return
        [
            new[] { new Booking(HotelId, midDate, midDate, RoomType), new Booking(HotelId, midDate.AddDays(3), midDate.AddDays(3), RoomType),},
            1,
            new SearchQuery(HotelId, startDate, endDate, RoomType),
            new SearchQueryResult(HotelId, RoomType, [
                (startDate, midDate.AddDays(-1)),
                (midDate.AddDays(1), midDate.AddDays(2)),
                (midDate.AddDays(4), endDate),
            ])
        ];

        // 2 rooms
        // one busy day
        yield return
        [
            new[] { new Booking(HotelId, startDate, startDate, RoomType), },
            2,
            new SearchQuery(HotelId, startDate, endDate, RoomType),
            new SearchQueryResult(HotelId, RoomType, [(startDate, endDate)])
        ];
        // week with two short booking
        yield return
        [
            new[] { new Booking(HotelId, startDate, startDate, RoomType), new Booking(HotelId, startDate.AddDays(3), startDate.AddDays(4), RoomType),},
            2,
            new SearchQuery(HotelId, startDate, endDate, RoomType),
            new SearchQueryResult(HotelId, RoomType, [(startDate, endDate)])
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
            new SearchQueryResult(HotelId, RoomType, [(startDate, endDate)])
        ];
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    private const string HotelId = "1";
    private const string RoomType = "STD";
}