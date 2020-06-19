using System;
using System.Collections.Generic;

namespace TurtleChallenge.Models
{
    /// <summary>
    /// The board validation result.
    /// </summary>
    public class BoardValidationResult
    {
        /// <summary>
        /// Creates a new instance of <see cref="BoardValidationResult"/>.
        /// </summary>
        /// <param name="validationMessages">The validation messages.</param>
        public BoardValidationResult(IList<string> validationMessages)
        {
            if (validationMessages is null)
            {
                throw new ArgumentNullException(nameof(validationMessages));
            }

            ValidationMessages = validationMessages;
            if (validationMessages.Count > 0)
            {
                IsValid = false;
            }
            else
            {
                IsValid = true;
            }
        }

        /// <summary>
        /// The validation success flag.
        /// </summary>
        public bool IsValid { get; }

        /// <summary>
        /// The collection of validation messages.
        /// </summary>
        public IEnumerable<string> ValidationMessages { get; }
    }
}
