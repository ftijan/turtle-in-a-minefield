using System.Collections.Generic;

namespace TurtleChallenge.Models
{
    public class MoveSequence
    {        
        public IEnumerable<IEnumerable<MoveType>> AllMoves { get; set; }
    }
}
