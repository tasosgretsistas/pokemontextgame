using PokemonTextEdition.Classes;
using PokemonTextEdition.Engine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace PokemonTextEdition
{
    public class Program
    {
        #region Constants & Variables

        //Current game version.
        public const string version = "v0.2 [BETA]";

        //The platform the program is running on.
        public const string platform = "console";

        //This parameter determines how important a message needs to be in order to get logged.
        //0 = trivial, 1 = important, 2 = vital.
        private static int logLevel = 1;

        //Developer mode.
        public static bool GodMode { get; set; }

        //The font colour for each kind of message. (If I ever decide to implement this)
        public static ConsoleColor eventColor = ConsoleColor.Yellow;

        #endregion

        #region Main Menu

        static void Main(string[] args)
        {
            //Introduction code. Keep this updated!

            Console.WriteLine("Welcome to Pokemon Red/Blue: Text Edition, by Tasos Gretsistas! " + version);
            Console.WriteLine("All 151 Pokemon are now in the game! Hurray!");
            Console.WriteLine("Pokemon can now evolve and you can use items! :-)");
            Console.WriteLine("");

            Console.WriteLine("A letter in parentheses represents a command shortcut. For instance, (f)ight");
            Console.WriteLine("means that you only need to type \"f\" to input this particular command.");
            Console.WriteLine("");

            MainMenu();
        }

        /// <summary>
        /// The game's main menu.
        /// </summary>
        public static void MainMenu()
        {
            Console.WriteLine("Type \"(l)oad\" to load game, \"(s)kip\" to skip the intro or press enter to begin.");

            string input = Console.ReadLine();

            if (input != "")
                Console.WriteLine();

            switch (input.ToLower())
            {
                case "skip":
                case "s":
                    SkipIntro();

                    break;

                case "load":
                case "l":
                    SaveLoad.Load();

                    break;

                case "testbattle":
                    Cheats.TestBattle();

                    break;

                case "list items":
                    ItemList.ListAllItems();
                    MainMenu();
                    break;

                case "list pokemon":
                    PokemonList.ListAllPokemon();
                    MainMenu();
                    break;

                case "list moves":
                    MovesList.ListAllMoves();
                    MainMenu();
                    break;

                default:
                    Story.Introduction();

                    break;
            }
        }

        /// <summary>
        /// Skips the story's introduction segment and automatically instantiates the Overworld.player and Overworld.rival objects with their default names.
        /// </summary>
        static void SkipIntro()
        {
            Console.WriteLine("By default the player will be named \"{0}\" and the rival \"{1}\".\n", Overworld.player.Name, Overworld.player.RivalName);

            Story.SelectPokemon();
        }

        #endregion

        #region Game Functionality

        /// <summary>
        /// Logging method that writes a given message with a given importance into a file called "log.txt".
        /// Messages with a lower importance than the threshold set at Program.cs will not be logged.
        /// </summary>
        /// <param name="message">The string of the actual message to be written.</param>
        /// <param name="messageLevel">The importance of the message. 0 = trivial, 1 = important, 2 = vital.</param>
        public static void Log(string message, int messageLevel)
        {
            //Simple log method that writes things into "log.txt".

            using (StreamWriter writer = new StreamWriter("log.txt", true))
            {
                //If the message's level of importance is higher than the current logLevel, it gets written. Otherwise, it gets ignored.

                if (messageLevel >= logLevel)
                    writer.WriteLine("{0} - {1}", DateTime.Now, message);
            }
        }

        #endregion

    }
}

