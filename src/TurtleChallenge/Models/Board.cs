using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TurtleChallenge.Models
{
    public class Board
    {
        [JsonPropertyName("tiles")]
        public IEnumerable<MoveType> Tiles { get; set; }
    }
}
