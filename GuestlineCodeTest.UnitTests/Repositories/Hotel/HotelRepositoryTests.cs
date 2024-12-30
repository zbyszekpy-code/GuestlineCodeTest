using GuestlineCodeTest.Repositories.InputModel;

namespace GuestlineCodeTest.UnitTests.Repositories.Hotel;

public class HotelRepositoryTests
{
    [Theory]
    [ClassData(typeof(HotelRepositoryTestData))]
    public void GetHotelsTest(IReadOnlyCollection<HotelDto> hotelDto, string hotelId, string roomType, int expectedResult)
    {
        // Arrange
        var hotelRepository = new GuestlineCodeTest.Repositories.HotelRepository(hotelDto);

        // Act
        var result = hotelRepository.GetRoomCount(hotelId, roomType);

        // Assert
        result.Should().Be(expectedResult);
    }
}
