using System.Text.Json.Serialization;

namespace TurtleChallenge.Models
{
    /// <summary>
    /// The board point data contract.
    /// </summary>
    public class Point
    {
        /// <summary>
        /// The X position on the game board.
        /// </summary>
        [JsonPropertyName("x")]
        public int X { get; set; }

        /// <summary>
        /// The Y position on the game board.
        /// </summary>
        [JsonPropertyName("y")]
        public int Y { get; set; }
    }
}
