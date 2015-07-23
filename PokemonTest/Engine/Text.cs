using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PokemonTextEdition.Engine
{
    class Text
    {
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
