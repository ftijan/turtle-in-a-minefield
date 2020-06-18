using System.Text.Json.Serialization;

namespace TurtleChallenge.Models
{
    public class DirectionPoint : Point
    {
        [JsonPropertyName("d")]
        public Direction Direction { get; set; }
    }
}
