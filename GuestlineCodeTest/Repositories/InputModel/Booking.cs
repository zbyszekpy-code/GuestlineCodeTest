using System.Text.Json.Serialization;

namespace GuestlineCodeTest.Repositories.InputModel;

public record BookingDto
{
    [JsonPropertyName("hotelId")]
    public string HotelId { get; set; }

    [JsonPropertyName("arrival")]
    public string Arrival { get; set; }

    [JsonPropertyName("departure")]
    public string Departure { get; set; }

    [JsonPropertyName("roomType")]
    public string RoomType { get; set; }

    [JsonPropertyName("roomRate")]
    public string RoomRate { get; set; }
}