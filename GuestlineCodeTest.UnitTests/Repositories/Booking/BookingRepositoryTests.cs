using GuestlineCodeTest.Repositories.InputModel;

namespace GuestlineCodeTest.UnitTests.Repositories.Booking;

public class BookingRepositoryTests
{
    [Theory]
    [ClassData(typeof(BookingRepositoryTestData))]
    public void GetBookingsTest(IReadOnlyCollection<BookingDto> bookingDto, string hotelId, string roomType, DateTime from, DateTime to, IReadOnlyCollection<Application.Booking> expectedResult)
    {
        // Arrange
        var bookingRepository = new GuestlineCodeTest.Repositories.BookingRepository(bookingDto);

        // Act
        var result = bookingRepository.GetBookings(hotelId, from, to, roomType);

        // Assert
        result.Should().BeEquivalentTo(expectedResult);
    }
}
