using System;
using System.Collections.Generic;
using System.Linq;
using TurtleChallenge.Models;

namespace TurtleChallenge
{
    /// <summary>
    /// Contains the turtle runner logic.
    /// </summary>
    public class TurtleRunner
    {
        /// <summary>
        /// The game board data.
        /// </summary>
        private Board Board { get; }        

        /// <summary>
        /// Creates an instance of <see cref="TurtleRunner"/> class.
        /// </summary>
        /// <param name="board">The game board data.</param>
        public TurtleRunner(Board board)
        {
            Board = board ?? throw new ArgumentNullException(nameof(board));
        }

        /// <summary>
        /// Executes the runner logic.
        /// </summary>
        /// <param name="moveSequence">The <see cref="MoveType"/> move sequence.</param>
        /// <returns>The run result.</returns>
        public RunResult Run(IEnumerable<MoveType> moveSequence)
        {
            if (moveSequence is null)
            {
                throw new ArgumentNullException(nameof(moveSequence));
            }

            ResetPosition();

            foreach (var move in moveSequence)
            {
                var result = Move(move);
                if (result == RunResult.Exit || result == RunResult.MineHit)
                {
                    return result;
                }
            }

            return RunResult.NotCleared;
        }

        /// <summary>
        /// Resets the position on the board.
        /// </summary>
        private void ResetPosition()
        {
            if (Board.CurrentPosition == null)
            {
                Board.CurrentPosition = new DirectionPoint();
            }

            Board.CurrentPosition.Direction = Board.StartingPoint.Direction;
            Board.CurrentPosition.X = Board.StartingPoint.X;
            Board.CurrentPosition.Y = Board.StartingPoint.Y;
        }

        /// <summary>
        /// Performs the move based on the move type.
        /// </summary>
        /// <param name="moveType">The move type.</param>
        /// <returns>The result.</returns>
        /// <remarks>
        /// A move can be a step in forward direction or a rotation.
        /// </remarks>
        private RunResult Move(MoveType moveType)
        {
            switch (moveType)
            {
                case MoveType.Move:
                    return StepInCurrentDirection();                    
                case MoveType.Rotate:
                    Rotate();
                    return RunResult.NotCleared;                    
                default:
                    throw new ArgumentException(nameof(moveType));                    
            }
        }

        /// <summary>
        /// Makes a move in the current direction based on the board position and orientation.
        /// </summary>
        /// <returns>The result of the move.</returns>
        private RunResult StepInCurrentDirection()
        {            
            switch (Board.CurrentPosition.Direction)
            {
                case Direction.North:
                    // y - 1
                    if (Board.CurrentPosition.Y - 1 < 0)
                    {
                        // skip turn
                        return RunResult.NotCleared;
                    }
                    else
                    {
                        Board.CurrentPosition.Y--;
                        return GetCurrentPositionResult();
                    }                    
                case Direction.East:
                    // x + 1
                    if (Board.CurrentPosition.X + 1 > Board.BoardSize.X)
                    {
                        // skip turn
                        return RunResult.NotCleared;
                    }
                    else
                    {
                        Board.CurrentPosition.X++;
                        return GetCurrentPositionResult();
                    }                    
                case Direction.South:
                    // y + 1
                    if (Board.CurrentPosition.Y + 1 > Board.BoardSize.Y)
                    {
                        // skip turn
                        return RunResult.NotCleared;
                    }
                    else
                    {
                        Board.CurrentPosition.Y++;
                        return GetCurrentPositionResult();
                    }                    
                case Direction.West:
                    // x - 1
                    if (Board.CurrentPosition.X - 1 < 0)
                    {
                        // skip turn
                        return RunResult.NotCleared;
                    }
                    else
                    {
                        Board.CurrentPosition.X--;
                        return GetCurrentPositionResult();
                    }
                default:
                    throw new InvalidOperationException();
            }
        }

        /// <summary>
        /// Gets the current position result.
        /// </summary>
        /// <returns>The result.</returns>
        /// <remarks>
        /// The calculated current position result indicates whether the game is won,
        /// lost or is inconclusive after this turn.
        /// </remarks>
        private RunResult GetCurrentPositionResult()
        {
            if (Board.CurrentPosition.X == Board.ExitPoint.X && Board.CurrentPosition.Y == Board.ExitPoint.Y)
            {
                return RunResult.Exit;
            }

            if (Board.Mines.Any(m => m.X == Board.CurrentPosition.X && m.Y == Board.CurrentPosition.Y))
            {
                return RunResult.MineHit;
            }

            return RunResult.NotCleared;
        }

        /// <summary>
        /// Rotates the position on the board by 90 degrees to the right (clockwise).
        /// </summary>
        private void Rotate()
        {
            switch (Board.CurrentPosition.Direction)
            {
                case Direction.North:
                    Board.CurrentPosition.Direction = Direction.East;
                    break;
                case Direction.East:
                    Board.CurrentPosition.Direction = Direction.South;
                    break;
                case Direction.South:
                    Board.CurrentPosition.Direction = Direction.West;
                    break;
                case Direction.West:
                    Board.CurrentPosition.Direction = Direction.North;
                    break;
                default:
                    break;
            }
        }
    }
}
