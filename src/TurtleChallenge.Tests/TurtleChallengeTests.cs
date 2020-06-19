using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Reflection;
using TurtleChallenge.Models;

namespace TurtleChallenge.Tests
{
    /// <summary>
    /// Runs the turtle challenge tests.
    /// </summary>
    [TestClass]
    public class TurtleChallengeTests
    {
        /// <summary>
        /// The game board.
        /// </summary>
        private Board Board { get; set; }

        /// <summary>
        /// The collection of move sequences.
        /// </summary>
        private IList<IEnumerable<MoveType>> AllMoves { get; set; }

        /// <summary>
        /// Initializes the test settings.
        /// </summary>
        [TestInitialize]
        public void Setup()
        {
            using (var resource = Assembly.GetExecutingAssembly()
                .GetManifestResourceStream("TurtleChallenge.Tests.Resources.game-settings.json"))
            {
                (Board, _) = Extensions.GetFromJson<Board>(resource);
            }

            using (var resource = Assembly.GetExecutingAssembly()
                .GetManifestResourceStream("TurtleChallenge.Tests.Resources.moves.json"))
            {
                (AllMoves, _) = Extensions.GetFromJson<IList<IEnumerable<MoveType>>>(resource);
            }
        }

        /// <summary>
        /// Tests whether the mine hit state will be successfully evaluated
        /// for a given sequence of moves.
        /// </summary>
        [TestMethod]
        public void Can_Hit_Mine()
        {
            // Arrange
            var runner = new TurtleRunner(Board);

            // Act
            var result = runner.Run(AllMoves[0]);

            // Assert
            Assert.AreEqual(RunResult.MineHit, result);
        }

        /// <summary>
        /// Tests whether the field not cleared state will be successfully evaluated
        /// for a given sequence of moves.
        /// </summary>
        [TestMethod]
        public void Can_Get_Stuck_In_Field()
        {
            // Arrange
            var runner = new TurtleRunner(Board);

            // Act
            var result = runner.Run(AllMoves[1]);

            // Assert
            Assert.AreEqual(RunResult.NotCleared, result);
        }

        /// <summary>
        /// Tests whether the field cleared state will be successfully evaluated
        /// for a given sequence of moves.
        /// </summary>
        [TestMethod]
        public void Can_Escape_Field()
        {
            // Arrange
            var runner = new TurtleRunner(Board);

            // Act
            var result = runner.Run(AllMoves[2]);

            // Assert
            Assert.AreEqual(RunResult.Exit, result);
        }
    }
}
