using System.Collections;
using GuestlineCodeTest.Repositories.InputModel;

namespace GuestlineCodeTest.UnitTests.Repositories.Hotel;

public class HotelRepositoryTestData : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        // one room
        yield return
        [
            new[]
            {
                new HotelDto
                {
                    Id = HotelId,
                    Rooms = new []
                    {
                        new RoomDto
                        {
                            RoomType = RoomStdType,
                        }
                    }
                },
            },
            HotelId,
            RoomStdType,
            1
        ];
        // one room but different type
        yield return
        [
            new[]
            {
                new HotelDto
                {
                    Id = HotelId,
                    Rooms = new []
                    {
                        new RoomDto
                        {
                            RoomType = RoomStdType,
                        }
                    }
                },
            },
            HotelId,
            RoomDblType,
            0
        ];

        // two room
        yield return
        [
            new[]
            {
                new HotelDto
                {
                    Id = HotelId,
                    Rooms = new []
                    {
                        new RoomDto
                        {
                            RoomType = RoomStdType,
                        },
                        new RoomDto
                        {
                            RoomType = RoomStdType,
                        }
                    }
                },
            },
            HotelId,
            RoomStdType,
            2
        ];

        // two room among mixed rooms
        yield return
        [
            new[]
            {
                new HotelDto
                {
                    Id = HotelId,
                    Rooms = new []
                    {
                        new RoomDto
                        {
                            RoomType = RoomStdType,
                        },
                        new RoomDto
                        {
                            RoomType = RoomStdType,
                        },
                        new RoomDto
                        {
                            RoomType = RoomDblType,
                        },
                        new RoomDto
                        {
                            RoomType = RoomDblType,
                        }
                    }
                },
            },
            HotelId,
            RoomStdType,
            2
        ];
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    private const string HotelId = "H1";
    private const string RoomStdType = "STD";
    private const string RoomDblType = "DBL";
}