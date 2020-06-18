using System.Text.Json.Serialization;

namespace TurtleChallenge.Models
{
    public class Tile
    {
        [JsonPropertyName("x")]
        public int X { get; set; }

        [JsonPropertyName("y")]
        public int Y { get; set; }

        [JsonPropertyName("type")]
        public TileType TileType { get; set; }
    }
}
