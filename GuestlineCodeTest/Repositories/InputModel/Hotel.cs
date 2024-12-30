using System.Text.Json.Serialization;

namespace GuestlineCodeTest.Repositories.InputModel;

public partial class HotelDto
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

public partial class RoomTypeDto
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

public partial class RoomDto
{
    [JsonPropertyName("roomType")]
    public string RoomType { get; set; }

    [JsonPropertyName("roomId")]
    public string RoomId { get; set;}
}

