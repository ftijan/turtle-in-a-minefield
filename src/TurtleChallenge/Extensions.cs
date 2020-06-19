using System.Collections.Generic;
using System.Linq;
using TurtleChallenge.Models;

namespace TurtleChallenge
{
    /// <summary>
    /// Extension methods container
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Validates the board settings.
        /// </summary>
        /// <param name="board">The board to validate.</param>
        /// <returns>The validation result.</returns>
        public static BoardValidationResult Validate(this Board board)
        {
            var validationMessages = new List<string>();
            
            if (board.BoardSize.X < 2)
            {
                validationMessages.Add("Field size: X smaller than 2");
            }

            if (board.BoardSize.Y < 2)
            {
                validationMessages.Add("Field size: Y smaller than 2");
            }

            if (board.StartingPoint.X >= board.BoardSize.X || board.StartingPoint.X < 0)
            {
                validationMessages.Add("Starting Point: X outside of bounds");
            }

            if (board.StartingPoint.Y >= board.BoardSize.Y || board.StartingPoint.Y < 0)
            {
                validationMessages.Add("Starting Point: Y outside of bounds");
            }

            if (board.ExitPoint.X >= board.BoardSize.X || board.ExitPoint.X < 0)
            {
                validationMessages.Add("Exit Point: X outside of bounds");
            }

            if (board.ExitPoint.Y >= board.BoardSize.Y || board.ExitPoint.Y < 0)
            {
                validationMessages.Add("Exit Point: Y outside of bounds");
            }

            if(board.Mines.Any(m => m.X >= board.BoardSize.X || m.X < 0))
            {
                validationMessages.Add("One or more mines: X outside of bounds");
            }

            if (board.Mines.Any(m => m.Y >= board.BoardSize.Y || m.Y < 0))
            {
                validationMessages.Add("One or more mines: Y outside of bounds");
            }

            if (board.Mines.Any(m => m.X == board.StartingPoint.X && m.Y == board.StartingPoint.Y))
            {
                validationMessages.Add("Starting Point position overlaps with one or more mines.");
            }

            if (board.Mines.Any(m => m.X == board.ExitPoint.X && m.Y == board.ExitPoint.Y))
            {
                validationMessages.Add("Exit Point position overlaps with one or more mines.");
            }

            return new BoardValidationResult(validationMessages);
        }
    }
}
