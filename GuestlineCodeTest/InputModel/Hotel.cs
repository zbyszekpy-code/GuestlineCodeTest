using System.Text.Json.Serialization;

namespace GuestlineCodeTest.InputModel;

public partial class Hotel
{
    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("roomTypes")]
    public RoomType[] RoomTypes { get; set; }

    [JsonPropertyName("rooms")]
    public Room[] Rooms { get; set; }
}

public partial class RoomType
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

public partial class Room
{
    [JsonPropertyName("roomType")]
    public string RoomType { get; set; }

    [JsonPropertyName("roomId")]
    public long RoomId { get; set;}
}

