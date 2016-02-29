using PokemonTextEdition.Classes;
using System;
using System.IO;

namespace PokemonTextEdition.Engine
{
    class Program
    {
        #region Main Menu

        static void Main(string[] args)
        {
            //Introduction code. Keep this updated!

            UI.WriteLine("Welcome to Pokemon Red/Blue: Text Edition, by Tasos Gretsistas! " + Settings.Game_Version);
            UI.WriteLine("All 151 Pokemon are now in the game! Hurray!");
            UI.WriteLine("Pokemon can now evolve and you can use items! :-)\n");

            UI.WriteLine("A letter in parentheses represents a command shortcut. For instance, (f)ight");
            UI.WriteLine("means that you only need to type \"f\" to input this particular command.\n");

            MainMenu();
        }

        /// <summary>
        /// The game's main menu.
        /// </summary>
        public static void MainMenu()
        {
            UI.WriteLine("Type \"(l)oad\" to load game, \"(s)kip\" to skip the intro or press enter to begin.");

            string input = UI.ReceiveInput();

            switch (input.ToLower())
            {
                case "":
                    Overworld.Player = new Player();

                    Story.Introduction();

                    break;

                case "skip":
                case "s":
                    SkipIntro();

                    break;

                case "load":
                case "l":
                    SaveLoad.Load();

                    break;

                case "god mode":
                    Cheats.GodMode();
                    MainMenu();
                    break;
                    
                case "testbattle":
                case "list pokemon":
                case "list pokemon bst":
                case "list pokemon evolution":
                case "list moves":
                case "list items":
                    Cheats.Authentication(input.ToLower());
                    MainMenu();
                    break;

                default:
                    UI.InvalidInput();
                    MainMenu();

                    break;
            }
        }

        /// <summary>
        /// Skips the story's introduction segment and automatically instantiates the Overworld.player and Overworld.rival objects with their default names.
        /// </summary>
        static void SkipIntro()
        {
            Overworld.Player = new Player();

            UI.WriteLine("By default the player will be named \"" + Overworld.Player.Name + "\" and the rival \"" + Overworld.Player.RivalName + "\".\n");

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

                if (messageLevel >= Settings.Program_LogLevel)
                    writer.WriteLine("{0} - {1}", DateTime.Now, message);
            }
        }

        #endregion

    }
}

