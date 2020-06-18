using System;
using System.Collections.Generic;
using System.Linq;
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

        private void ResetPosition()
        {
            Board.CurrentPosition.Direction = Board.StartingPoint.Direction;
            Board.CurrentPosition.X = Board.StartingPoint.X;
            Board.CurrentPosition.Y = Board.StartingPoint.Y;
        }

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

        private RunResult StepInCurrentDirection()
        {            
            switch (Board.CurrentPosition.Direction)
            {
                case Direction.North:
                    // y - 1
                    if (Board.CurrentPosition.Y - 1 <= 0)
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
                    if (Board.CurrentPosition.X + 1 >= Board.BoardSize.X)
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
                    if (Board.CurrentPosition.Y + 1 >= Board.BoardSize.Y)
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
                    if (Board.CurrentPosition.X - 1 <= 0)
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
