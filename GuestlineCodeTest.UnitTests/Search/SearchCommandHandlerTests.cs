using GuestlineCodeTest.Application.Search;

namespace GuestlineCodeTest.UnitTests.Search;

public class SearchCommandHandlerTests
{
    [Theory]
    [ClassData(typeof(SearchTestData))]
    public void SearchQueryTest(IReadOnlyCollection<Booking> booking, int maxRoomCount, SearchQuery query, SearchQueryResult expectedResult)
    {
        // Arrange
        var bookingRepository = new Mock<IBookingRepository>();
        bookingRepository.Setup(x => x.GetBookings(query.HotelId, query.From, query.To, query.RoomType)).Returns(booking);
        var hotelsRepository = new Mock<IHotelRepository>();
        hotelsRepository.Setup(x => x.GetRoomCount(query.HotelId, query.RoomType)).Returns(maxRoomCount);

        var handler = new SearchCommandHandler(hotelsRepository.Object, bookingRepository.Object);

        // Act
        var result = handler.Handle(query);

        // Assert
        result.Should().BeEquivalentTo(expectedResult);
    }
}
