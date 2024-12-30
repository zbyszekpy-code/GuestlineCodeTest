using System.Text.Json.Serialization;
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

namespace GuestlineCodeTest.Repositories.InputModel;

public class HotelDto
{
    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("roomTypes")]
    public RoomTypeDto[] RoomTypes { get; set; }

    [JsonPropertyName("rooms")]
    public RoomDto[] Rooms { get; set; }
}

public class RoomTypeDto
{
    [JsonPropertyName("code")]
    public string Code { get; set; }

    [JsonPropertyName("description")]
    public string Description { get; set; }

    [JsonPropertyName("amenities")]
    public string[] Amenities { get; set; }

    [JsonPropertyName("features")]
    public string[] Features { get; set; }
}

public class RoomDto
{
    [JsonPropertyName("roomType")]
    public string RoomType { get; set; }

    [JsonPropertyName("roomId")]
    public string RoomId { get; set;}
}
