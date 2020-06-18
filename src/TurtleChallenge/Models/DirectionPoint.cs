using System.Text.Json.Serialization;

namespace TurtleChallenge.Models
{
    /// <summary>
    /// The point with orientation on the game board.
    /// </summary>
    public class DirectionPoint : Point
    {
        /// <summary>
        /// The direction.
        /// </summary>
        [JsonPropertyName("d")]
        public Direction Direction { get; set; }
    }
}
