using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TurtleChallenge.Models
{
    /// <summary>
    /// The game board data contract.
    /// </summary>
    public class Board
    {
        /// <summary>
        /// The board size.
        /// </summary>
        /// <remarks>
        /// X indicates the X (horizontal) size (starting from 1), 
        /// Y indicates Y (vertical) size (likewise starting from 1).
        /// </remarks>
        [JsonPropertyName("boardSize")]
        public Point BoardSize { get; set; }

        /// <summary>
        /// The starting point.
        /// </summary>
        /// <remarks>
        /// X and Y are zero-based location indices.
        /// </remarks>
        [JsonPropertyName("startingPoint")]
        public DirectionPoint StartingPoint { get; set; }

        /// <summary>
        /// The exit point.
        /// </summary>
        /// <remarks>
        /// X and Y are zero-based location indices.
        /// </remarks>
        [JsonPropertyName("exitPoint")]
        public Point ExitPoint { get; set; }

        /// <summary>
        /// The mines locations array.
        /// </summary>
        /// <remarks>
        /// X and Y are zero-based location indices.
        /// </remarks>
        [JsonPropertyName("mines")]
        public IList<Point> Mines { get; set; } = new List<Point>();

        /// <summary>
        /// The current position.
        /// </summary>
        /// <remarks>
        /// X and Y are zero-based location indices.
        /// Not used for JSON data load.
        /// </remarks>
        [JsonIgnore]
        public DirectionPoint CurrentPosition { get; set; }
    }
}
