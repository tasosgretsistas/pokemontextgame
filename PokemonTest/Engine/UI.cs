using System;
using System.Linq;

namespace PokemonTextEdition.Engine
{
    /// <summary>
    /// This class handles interfacing with the user.
    /// In the console version, it primarily deals with displaying messages to the user, as well as receiving input.
    /// </summary>
    class UI
    {
        /// <summary>
        /// Displays a message to the player followed by a line break.
        /// </summary>
        /// <param name="message">The message to display.</param>
        public static void WriteLine(string message)
        {
            Console.WriteLine(message);
        }

        /// <summary>
        /// Displays a message to the player, without a line break at the end.
        /// </summary>
        /// <param name="message">The message to display.</param>
        public static void Write(string message)
        {
            Console.Write(message);
        }

        /// <summary>
        /// Displays an error message to the player, and optionally logs another message for the developer to see.
        /// </summary>
        /// <param name="message">The message to display to the user. Leave blank if it is desirable for the user not to be notified of the error's occurance.</param>
        /// <param name="debugInfo">The message to log to the log.txt file. Leave blank if is not necessary for the error to be logged.</param>
        /// <param name="errorLevel">The significance of the error - 0 (trivial), 1 (important), 2 (vital)</param>
        public static void Error(string message, string debugInfo, int errorLevel)
        {
            if (message != "")
            {
                WriteLine("Error: " + message);
                WriteLine("Please contact the author with your log.txt file so he can fix it. :|");
            }

            if (debugInfo != "")
                Program.Log(debugInfo, errorLevel);
        }

        /// <summary>
        /// Receives input from the player and then returns the input in the form of a string.
        /// </summary>
        /// <returns>The user's input as a string.</returns>
        public static string ReceiveInput()
        {
            string input = Console.ReadLine();

            if (!input.Equals(string.Empty))
               WriteLine("");

            return input;
        }

        public static string ReceiveKey()
        {
            string input = Console.ReadKey(true).Key.ToString().ToLower();

            UI.WriteLine("");

            return input;
        }


        /// <summary>
        /// Receives input from the player and then attempts to convert it to an integer number through the "out result" parameter, returning true if the conversion was possible.
        /// I couldn't get this to do anything meaningful that the <c>Int32.TryParse</c> method doesn't do already so I decided against using it for the time being.
        /// </summary>
        /// <param name="displayError">Determines whether an error will be displayed in the event that the conversion was unsuccessful.</param>
        /// <param name="result">The resulting number if the operation was successful, or 0 if it was not.</param>
        /// <returns>True if the operation was successful, or 0 if it was not.</returns>
        public static bool ReceiveNumber(bool displayError, out int result)
        {
            string input = ReceiveInput();

            int number;
            
            bool validInput = int.TryParse(input, out number);

            if (validInput)
            {
                result = number;
                return true;
            }               

            else
            {
                if (displayError)
                    InvalidInput();

                result = 0;
                return false;
            }
        }

        /// <summary>
        /// Defunct method, used for receiving input and then processing the input. Will use it again if I change back to delegates for menu options.
        /// </summary>
        /// <param name="displayMessage"></param>
        /// <param name="validInput"></param>
        /// <returns></returns>
        public static string UserInput(string displayMessage, string[] validInput)//, bool mandatorySelection, bool displayError, bool numbersOnly)
        {
            string input = null;

            do
            {
                if (input != null) // && displayError)
                    InvalidInput();

                WriteLine(displayMessage);

                input = ReceiveInput();
            }            

            while (!validInput.Contains(input.ToLower())); // && mandatorySelection)            

            return input;
        }

        /// <summary>
        /// Asks the user to press any key in order for the program to continue. Used for breaking up big chunks of text in order to provide a better player experience.
        /// </summary>
        public static void AnyKey()
        {
            WriteLine("\nPress any key to continue.");

            Console.ReadKey(true);

            WriteLine("");
        }

        /// <summary>
        /// Displays a message saying that the user's input was invalid.
        /// </summary>
        public static void InvalidInput()
        {
            WriteLine("Invalid input.\n");
        }
    }
}
