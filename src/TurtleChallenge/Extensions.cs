using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using TurtleChallenge.Models;

namespace TurtleChallenge
{
    /// <summary>
    /// Extension methods container
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// The JSON deserializer options.
        /// </summary>
        public static JsonSerializerOptions DeserializerOptions { get; set; }

        /// <summary>
        /// Runs the initialization of <see cref="Extensions"/> class on first invoke.
        /// </summary>
        static Extensions()
        {
            DeserializerOptions = new JsonSerializerOptions();
            DeserializerOptions.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));
        }

        /// <summary>
        /// Reads data from json, given the file name and the type to deserialize to.
        /// </summary>
        /// <typeparam name="T">The type to deserialize the data to.</typeparam>
        /// <param name="fileName">The file name to read.</param>
        /// <returns>Deserialized data.</returns>
        public static (T, bool) GetFromJson<T>(string fileName)
        {
            try
            {
                var jsonString = File.ReadAllText(fileName);                

                var data = JsonSerializer.Deserialize<T>(jsonString, DeserializerOptions);
                return (data, true);
            }
            catch (JsonException)
            {
                return (default(T), false);
            }
        }

        /// <summary>
        /// Reads data from json, given the source stream and the type to deserialize to.
        /// </summary>
        /// <typeparam name="T">The type to deserialize the data to.</typeparam>
        /// <param name="fileName">The stream to read.</param>
        /// <returns>Deserialized data.</returns>
        public static (T, bool) GetFromJson<T>(Stream resourceStream)
        {
            try
            {
                using var reader = new StreamReader(resourceStream);
                var jsonString = reader.ReadToEnd();

                var data = JsonSerializer.Deserialize<T>(jsonString, DeserializerOptions);
                return (data, true);
            }
            catch (JsonException)
            {
                return (default(T), false);
            }
        }

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
