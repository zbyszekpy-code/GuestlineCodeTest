namespace GuestlineCodeTest.UnitTests.Availability;

public class AvailabilityCommandHandlerTests
{
    [Theory]
    [ClassData(typeof(AvailabilityTestData))]
    public void Test1(IReadOnlyCollection<Booking> booking, int maxRoomCount, AvailabilityQuery query, AvailabilityQueryResult expectedResult)
    {
        // Arrange
        var bookingRepository = new Mock<IBookingRepository>();
        bookingRepository.Setup(x => x.GetBookings(query.HotelId, query.From, query.To, query.RoomType)).Returns(booking);
        var hotelsRepository = new Mock<IHotelsRepository>();
        hotelsRepository.Setup(x => x.GetRoomCount(query.HotelId, query.RoomType)).Returns(maxRoomCount);

        var handler = new AvailabilityCommandHandler(hotelsRepository.Object, bookingRepository.Object);

        // Act
        var result = handler.Handle(query);

        // Assert
        result.Should().BeEquivalentTo(expectedResult);
    }
}