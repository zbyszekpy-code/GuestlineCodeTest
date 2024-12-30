using System.Text.Json.Serialization;
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

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