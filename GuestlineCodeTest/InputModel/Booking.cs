using System.Text.Json.Serialization;

namespace GuestlineCodeTest.InputModel;

public record Booking
{
    [JsonPropertyName("hotelId")]
    public string HotelId { get; set; }

    [JsonPropertyName("arrival")]
    public long Arrival { get; set; }

    [JsonPropertyName("departure")]
    public long Departure { get; set; }

    [JsonPropertyName("roomType")]
    public string RoomType { get; set; }

    [JsonPropertyName("roomRate")]
    public string RoomRate { get; set; }
}