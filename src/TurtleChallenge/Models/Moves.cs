using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TurtleChallenge.Models
{
    public class MoveSequence
    {
        [JsonPropertyName("moves")]
        public IEnumerable<MoveType> Moves { get; set; }
    }
}
