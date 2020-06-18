using System;
using TurtleChallenge.Models;

namespace TurtleChallenge
{
    public class TurtleRunner
    {
        private Board Board { get; }

        public TurtleRunner(Board board)
        {
            Board = board ?? throw new ArgumentNullException(nameof(board));
        }

        public RunResult Run(MoveSequence moves)
        {
            if (moves is null)
            {
                throw new ArgumentNullException(nameof(moves));
            }

            throw new NotImplementedException();
        }        
    }
}
