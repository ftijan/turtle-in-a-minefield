using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using TurtleChallenge.Models;

namespace TurtleChallenge
{
    /// <summary>
    /// The program.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Runs the app.
        /// </summary>
        /// <param name="args">The input arguments array.</param>
        static void Main(string[] args)
        {
            Console.WriteLine("TurtleChallenge\n");

            if (!CheckArguments(args))
            {
                PrintArgumentsMessage();
                return;
            }

            (Board boardData, bool boardReadResult) = GetFromJson<Board>(args[0]);

            if (boardReadResult == false)
            {
                Console.WriteLine("Game settings file contains invalid JSON. Exiting...");
                return;
            }

            (IList<IEnumerable<MoveType>> movesData, bool movesReadResult) = GetFromJson<IList<IEnumerable<MoveType>>>(args[1]);

            if (movesReadResult == false)
            {
                Console.WriteLine("Moves file contains invalid JSON. Exiting...");
                return;
            }

            var boardValidationResult = boardData.Validate();

            if (boardValidationResult.IsValid == false)
            {
                Console.WriteLine("Game settings are invalid.");

                foreach (var message in boardValidationResult.ValidationMessages)
                {
                    Console.WriteLine(message);
                }

                return;
            }

            var turtleRunner = new TurtleRunner(boardData);

            for (int i = 0; i < movesData.Count; i++)
            {
                var result = turtleRunner.Run(movesData[i]);
                Console.WriteLine($"Sequence {i + 1}: {GetResultString(result)}!");                
            }
        }

        /// <summary>
        /// Gets the displayable result string from enum result.
        /// </summary>
        /// <param name="runResult">The run result enum value.</param>
        /// <returns>The result string.</returns>
        private static string GetResultString(RunResult runResult)
        {
            return runResult switch
            {
                RunResult.Exit => "Success",
                RunResult.MineHit => "Mine hit",
                RunResult.NotCleared => "Still in danger",
                _ => throw new ArgumentException(nameof(runResult)),
            };
        }

        /// <summary>
        /// Reads data from json, given the file name and the type to deserialze to.
        /// </summary>
        /// <typeparam name="T">The type to deserialize the data to.</typeparam>
        /// <param name="fileName">The file name to read.</param>
        /// <returns>Deserialized data.</returns>
        private static (T, bool) GetFromJson<T>(string fileName)
        {
            try
            {
                var jsonString = File.ReadAllText(fileName);

                var deserializerOptions = new JsonSerializerOptions();
                deserializerOptions.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));

                var data = JsonSerializer.Deserialize<T>(jsonString, deserializerOptions);
                return (data, true);
            }
            catch (JsonException)
            {                
                return (default(T), false);
            }
        }

        /// <summary>
        /// Checks whether the arguments are valid.
        /// </summary>
        /// <param name="args">The arguments array.</param>
        /// <returns>The validation flag.</returns>
        private static bool CheckArguments(string[] args)
        {
            if (args.Length != 2)
            {
                Console.WriteLine("Invalid number of arguments.");
                return false;
            }
            
            if (string.IsNullOrWhiteSpace(args[0]))
            {
                Console.WriteLine("Invalid argument: game settings file location.");
                return false;
            }

            if (!File.Exists(Path.GetFullPath(args[0])))
            {
                Console.WriteLine("Invalid argument: no file at given game settings file location.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(args[1]))
            {
                Console.WriteLine("Invalid argument: moves file location.");
                return false;
            }

            if (!File.Exists(Path.GetFullPath(args[1])))
            {
                Console.WriteLine("Invalid argument: no file at given moves file location.");
                return false;
            }

            return true;
        }

        /// <summary>
        /// Prints the error message for missing arguments.
        /// </summary>
        private static void PrintArgumentsMessage()
        {
            Console.WriteLine("\nExpected arguments:");
            Console.WriteLine("- Game settings file location");
            Console.WriteLine("- Moves file location");
        }
    }
}
