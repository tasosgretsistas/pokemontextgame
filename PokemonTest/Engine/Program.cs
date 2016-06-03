using PokemonTextEdition.Classes;
using System;
using System.IO;

namespace PokemonTextEdition.Engine
{
    class Program
    {
        /// <summary>
        /// This is the game's main random number generator. Any function that requires generic randomness will invoke this instance of Random.
        /// </summary>
        public static Random random = new Random(DateTime.Now.Second);

        #region Main Menu

        static void Main(string[] args)
        {
            //Introduction code. Keep this updated!

            Log("---------- NEW SESSION ----------", 1);

            string welcome = "Welcome to Pokemon Red/Blue: Text Edition v" + Settings.GameVersion + ", by Tasos Gretsistas!";

            string news = "This version features an engine clean-up and some new minor features.";

            string help = "A letter in square brackets represents a command shortcut. For instance, [F]ight\n" +
                          "means that you only need to press the \"F\" key to input this particular command.";

            UI.WriteLine(welcome + "\n" + news + "\n\n" + help + "\n");

            MainMenu();
        }

        /// <summary>
        /// The game's main menu.
        /// </summary>
        public static void MainMenu()
        {
            UI.WriteLine("[L]oad game, [S]kip introduction or press any other key to begin.");

            string input = UI.ReceiveKey();

            switch (input)
            {
                case "l":
                    SaveLoad.Load();
                    break;

                case "s":
                    SkipIntro();
                    break;

                case "c":
                    Cheats.CheatListener();
                    MainMenu();
                    break;

                case "enter":
                default:
                    Game.Player = new Player();
                    Story.Introduction();
                    break; 
            }
        }

        /// <summary>
        /// Skips the story's introduction segment and automatically instantiates the Game.Player and Overworld.rival objects with their default names.
        /// </summary>
        static void SkipIntro()
        {
            Game.Player = new Player();

            UI.WriteLine("By default the player will be named \"" + Game.Player.Name + "\" and the rival \"" + Story.rival.Name + "\".\n");

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

                if (messageLevel >= Settings.LogLevel)
                    writer.WriteLine("{0} - {1}", DateTime.Now, message);
            }
        }

        #endregion
    }
}

