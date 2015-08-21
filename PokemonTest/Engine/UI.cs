using System;
using System.Linq;

namespace PokemonTextEdition.Engine
{
    class UI
    {
        public static void WriteLine(string message)
        {
            Console.WriteLine(message);
        }

        public static void Write(string message)
        {
            Console.Write(message);
        }

        public static void Error(string message)
        {
            if (message != "")
                Console.WriteLine(message);

            Program.Log(message, 1);
        }

        public static string UserInput(string displayMessage, string[] validInput)//, bool mandatorySelection, bool displayError, bool numbersOnly)
        {
            string input = null;

            do
            {
                if (input != null) // && displayError)
                    Console.WriteLine("Invalid input. Please try again.\n");

                Console.WriteLine(displayMessage);

                input = Console.ReadLine();
            }

            

            while (!validInput.Contains(input.ToLower())); // && mandatorySelection)

            if (input != "")
                Console.WriteLine("");

            return input;
        }

        public static void AnyKey()
        {
            Console.WriteLine("\nPress any key to continue.");

            Console.ReadKey(true);

            Console.WriteLine("");
        }
    }
}
