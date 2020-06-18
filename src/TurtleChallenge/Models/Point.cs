using System.Text.Json.Serialization;

namespace TurtleChallenge.Models
{
    public class Point
    {
        [JsonPropertyName("x")]
        public int X { get; set; }

        [JsonPropertyName("y")]
        public int Y { get; set; }
    }
}
