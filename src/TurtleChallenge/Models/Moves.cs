using System.Collections.Generic;

namespace TurtleChallenge.Models
{
    /// <summary>
    /// The move sequence data contract.
    /// </summary>
    public class MoveSequence
    {        
        /// <summary>
        /// Collection of all move sequences.
        /// </summary>
        public IEnumerable<IEnumerable<MoveType>> AllMoves { get; set; }
    }
}
