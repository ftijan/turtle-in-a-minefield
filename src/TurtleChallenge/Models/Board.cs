using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TurtleChallenge.Models
{
    public class Board
    {
        [JsonPropertyName("boardSize")]
        public Point BoardSize { get; set; }

        [JsonPropertyName("startingPoint")]
        public DirectionPoint StartingPoint { get; set; }

        [JsonPropertyName("exitPoint")]
        public Point ExitPoint { get; set; }

        [JsonPropertyName("mines")]
        public IList<Point> Mines { get; set; } = new List<Point>();

        [JsonIgnore]
        public DirectionPoint CurrentPosition { get; set; }
    }
}
